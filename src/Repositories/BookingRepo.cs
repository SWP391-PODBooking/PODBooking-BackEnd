using BE.src.Domains.Database;
using BE.src.Domains.DTOs.Booking;
using BE.src.Domains.Enum;
using BE.src.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Crypto.Engines;
using ZstdSharp.Unsafe;

namespace BE.src.Repositories
{
    public interface IBookingRepo
    {
        Task<bool> AddBooking(Booking booking);
        Task<bool> AddBookingItems(List<BookingItem> bookingItems);
        Task<bool> UpdateBooking(Booking booking);
        Task<List<Booking>> ViewBookingOfRoomInFuture(Guid roomId);
        Task<Booking?> GetBookingById(Guid id);
        Task<List<Booking>> ViewBookingAvailablePeriod(Guid RoomId, DateTime StartDate, DateTime EndDate);
        Task<bool> AcceptBooking(Guid bookingId);
        Task<bool> DeclineBooking(Guid bookingId);
        Task<List<Booking>> GetBookingRequests();
        Task<List<BookingCheckAvailableDTO>> GetBookingCheckAvailableList(Guid bookingId);
        Task<bool> ProcessRefund(Guid bookingId);
        Task<bool> ProcessAcceptBooking(Guid bookingId);
        Task<Booking?> GetBookingWaitOrInProgressById(Guid id);
        Task<Booking?> CheckBookedRoom(Guid roomId, DateTime DateBooking, TimeSpan TimeBooking);
        Task<Booking?> CheckBookReqUser(Guid roomId, Guid userId, DateTime DateBooking, TimeSpan TimeBooking);
        Task<List<Booking>> GetScheduleBookingForStaff(DateTime startDate, DateTime endDate);
        Task<List<Booking>> GetBookingRequestsInProgressForStaff();
        Task<List<Booking>> ListBookingUserUpComing(Guid userId);
        Task<bool> CancleAllBookingByUser(Guid userId);
        Task<List<Booking>> GetBookingsWaitAccepted(Guid roomId);
        Task<List<Booking>> GetListBookingByAmenityService(Guid amenityServiceId);
        Task<bool> UpdateBookingItem(BookingItem bookingItem);
        Task<int> TotalBooking();
        Task<BookingItem?> GetBookingItemById(Guid BookingItemId);
        Task<List<Booking>> ScheduleRoom(Guid roomId, DateTime StartDate, DateTime EndDate);
    }
    public class BookingRepo : IBookingRepo
    {
        private readonly PodDbContext _context;
        public BookingRepo(PodDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddBookingItems(List<BookingItem> bookingItems)
        {
            _context.BookingItems.AddRange(bookingItems);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Booking?> GetBookingById(Guid id)
        {
            return await _context.Bookings
                                    .Include(b => b.PaymentRefunds.Where(p => p.Type == PaymentRefundEnum.Payment))
                                    .Include(b => b.BookingItems)
                                    .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> UpdateBooking(Booking booking)
        {
            _context.Update(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Booking>> ViewBookingOfRoomInFuture(Guid roomId)
        {
            return await _context.Bookings.Where(b => b.RoomId == roomId && b.DateBooking > DateTime.Now).ToListAsync();
        }

        public async Task<List<Booking>> ViewBookingAvailablePeriod(Guid RoomId, DateTime StartDate, DateTime EndDate)
        {
            return await _context.Bookings
                                    .Where(b => b.RoomId == RoomId
                                            && b.DateBooking >= StartDate && b.DateBooking <= EndDate
                                            && b.IsPay)
                                    .ToListAsync();
        }


        public async Task<bool> AcceptBooking(Guid bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);

            if (booking == null) return false;

            booking.Status = StatusBookingEnum.Accepted;
            booking.CreateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeclineBooking(Guid bookingId)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null) return false;

            booking.Status = StatusBookingEnum.Canceled;
            booking.CreateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Booking>> GetBookingRequests()
        {
            var bookingRequests = await _context.Bookings
                        .Include(b => b.BookingItems)
                            .ThenInclude(bi => bi.AmenityService)
                        .Include(b => b.Room)
                            .ThenInclude(r => r.Images)
                        .Where(b => b.Status == StatusBookingEnum.Wait)
                        .Select(b => new Booking
                        {
                            Id = b.Id,
                            Total = b.Total,
                            Status = b.Status,
                            IsPay = b.IsPay,
                            DateBooking = b.DateBooking,
                            TimeBooking = b.TimeBooking,
                            User = new User
                            {
                                Id = b.User.Id,
                                Name = b.User.Name,
                                Email = b.User.Email,
                                Phone = b.User.Phone,
                                Image = b.User.Image != null
                                    ? new Image
                                    {
                                        Id = b.User.Image.Id,
                                        Url = b.User.Image.Url
                                    }
                                    : null
                            },
                            Room = new Room
                            {
                                Id = b.Room.Id,
                                Name = b.Room.Name,
                                Images = b.Room.Images
                                    .OrderByDescending(i => i.CreateAt)
                                    .Take(1)
                                    .Select(i => new Image
                                    {
                                        Id = i.Id,
                                        Url = i.Url
                                    }).ToList()
                            },
                            BookingItems = b.BookingItems
                                .Select(bi => new BookingItem
                                {
                                    Id = bi.Id,
                                    AmountItems = bi.AmountItems,
                                    Total = bi.Total,
                                    AmenityService = new AmenityService
                                    {
                                        Id = bi.AmenityService.Id,
                                        Name = bi.AmenityService.Name
                                    }
                                }).ToList()
                        })
                        .ToListAsync();

            return bookingRequests.OrderByDescending(b => b.DateBooking.Add(b.TimeBooking)).ToList();
        }

        public async Task<List<BookingCheckAvailableDTO>> GetBookingCheckAvailableList(Guid bookingId)
        {
            var bookingAlreadyInProgress = await _context.Bookings
                .Where(b => b.Id == bookingId &&
                        b.Status == StatusBookingEnum.Wait ||
                        b.Status == StatusBookingEnum.Accepted)
                .Select(b => new BookingCheckAvailableDTO
                {
                    BookingId = b.Id,
                    TimeBooking = b.TimeBooking,
                    DateBooking = b.DateBooking,
                    Total = b.Total,
                    Status = b.Status,
                    IsPay = b.IsPay,
                    UserId = b.UserId,
                    RoomId = b.RoomId,
                    CreateAt = b.CreateAt
                })
                .FirstOrDefaultAsync();

            if (bookingAlreadyInProgress == null)
                return new List<BookingCheckAvailableDTO>();

            var bookingCheckAvailableList = _context.Bookings
                            .Where(b => b.Status == StatusBookingEnum.Wait)
                            .Select(b => new BookingCheckAvailableDTO
                            {
                                BookingId = b.Id,
                                TimeBooking = b.TimeBooking,
                                DateBooking = b.DateBooking,
                                Total = b.Total,
                                Status = b.Status,
                                IsPay = b.IsPay,
                                UserId = b.UserId,
                                RoomId = b.RoomId,
                                CreateAt = b.CreateAt,
                                UpdateAt = b.UpdateAt
                            })
                            .AsEnumerable()
                            .Where(b => bookingAlreadyInProgress.DateBooking.Date == b.DateBooking.Date &&
                                    bookingAlreadyInProgress.DateBooking.Add(bookingAlreadyInProgress.TimeBooking) < b.DateBooking.Add(b.TimeBooking))
                            .ToList();

            bool isAvailable = bookingCheckAvailableList
                .Any(b => bookingAlreadyInProgress.DateBooking >= b.DateBooking.Add(b.TimeBooking) &&
                          bookingAlreadyInProgress.DateBooking.Add(bookingAlreadyInProgress.TimeBooking) <= b.DateBooking);

            return !isAvailable ? bookingCheckAvailableList.ToList() : new List<BookingCheckAvailableDTO>();
        }

        public async Task<bool> ProcessRefund(Guid bookingId)
        {
            var booking = await GetBookingById(bookingId);
            if (booking == null)
            {
                return false;
            }
            PaymentRefund newRefund = new()
            {
                Type = PaymentRefundEnum.Refund,
                Total = booking.Total,
                PointBonus = 0,
                Status = true,
                IsRefundReturnRoom = true,
                BookingId = booking.Id
            };
            _context.PaymentRefunds.Add(newRefund);
            foreach (var bookingItem in booking.BookingItems)
            {
                RefundItem refundItem = new RefundItem()
                {
                    AmountItems = bookingItem.AmountItems,
                    Total = bookingItem.Total,
                    PaymentRefundId = newRefund.Id,
                    BookingItemId = bookingItem.Id
                };
                _context.RefundItems.Add(refundItem);
            }
            Transaction transaction = new()
            {
                TransactionType = TypeTransactionEnum.Refund,
                Total = booking.Total,
                PaymentRefundId = newRefund.Id,
                UserId = booking.UserId
            };
            _context.Transactions.Add(transaction);
            Notification notification = new()
            {
                Title = "Your booking has been reject",
                Description = "Your booking has been reject",
                UserId = booking.UserId
            };
            _context.Notifications.Add(notification);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == booking.UserId);
            if (user == null)
            {
                return false;
            }
            user.Wallet += newRefund.Total;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ProcessAcceptBooking(Guid bookingId)
        {
            //notification
            var booking = await GetBookingById(bookingId);
            if (booking == null)
            {
                return false;
            }
            var newNotification = new Notification()
            {
                Title = "Your Booking is Accepted",
                Description = "Your Booking is Accepted",
                UserId = booking.UserId,
            };

            _context.Notifications.Add(newNotification);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<Booking?> GetBookingWaitOrInProgressById(Guid id)
        {
            return await _context.Bookings
                                    .Include(b => b.PaymentRefunds)
                                    .FirstOrDefaultAsync(b => b.Id == id &&
                                    (b.Status == StatusBookingEnum.Wait || b.Status == StatusBookingEnum.Accepted));
        }

        public async Task<Booking?> CheckBookedRoom(Guid roomId, DateTime DateBooking, TimeSpan TimeBooking)
        {
            return _context.Bookings.AsEnumerable().FirstOrDefault(b => b.RoomId == roomId
                                    && b.Status == StatusBookingEnum.Accepted
                                    && (!(b.DateBooking.Add(b.TimeBooking) < DateBooking
                                    || DateBooking.Add(TimeBooking) < b.DateBooking)));
        }

        public async Task<Booking?> CheckBookReqUser(Guid roomId, Guid userId, DateTime DateBooking, TimeSpan TimeBooking)
        {
            return _context.Bookings.AsEnumerable().FirstOrDefault(b => b.RoomId == roomId
                                    && b.UserId == userId
                                    && b.Status == StatusBookingEnum.Wait
                                    && (!(b.DateBooking.Add(b.TimeBooking) < DateBooking
                                    || DateBooking.Add(TimeBooking) < b.DateBooking)));
        }

        public async Task<List<Booking>> GetScheduleBookingForStaff(DateTime startDate, DateTime endDate)
        {
            var bookings = await _context.Bookings
                                        .Where(b =>
                                            (b.Status == StatusBookingEnum.Accepted || b.Status == StatusBookingEnum.Done) &&
                                            b.DateBooking >= startDate &&
                                            b.DateBooking <= endDate)
                                        .Include(b => b.Room)
                                        .Include(b => b.User)
                                            .ThenInclude(u => u.Image)
                                        .Include(b => b.PaymentRefunds.Where(p => p.Type == PaymentRefundEnum.Payment))
                                        .ToListAsync();
            return bookings;
        }

        public async Task<List<Booking>> GetBookingRequestsInProgressForStaff()
        {
            var bookingRequests = await _context.Bookings
                        .Include(b => b.BookingItems)
                        .Include(b => b.BookingItems)
                            .ThenInclude(bi => bi.AmenityService)
                        .Include(b => b.Room)
                            .ThenInclude(r => r.Images)
                        .Where(b => b.Status == StatusBookingEnum.Accepted)
                        .Select(b => new Booking
                        {
                            Id = b.Id,
                            Total = b.Total,
                            Status = b.Status,
                            IsPay = b.IsPay,
                            DateBooking = b.DateBooking,
                            TimeBooking = b.TimeBooking,
                            User = new User
                            {
                                Id = b.User.Id,
                                Name = b.User.Name,
                                Email = b.User.Email,
                                Phone = b.User.Phone,
                                Image = b.User.Image != null
                                    ? new Image
                                    {
                                        Id = b.User.Image.Id,
                                        Url = b.User.Image.Url
                                    }
                                    : null
                            },
                            Room = new Room
                            {
                                Id = b.Room.Id,
                                Name = b.Room.Name,
                                Images = b.Room.Images
                                    .OrderByDescending(i => i.CreateAt)
                                    .Take(1)
                                    .Select(i => new Image
                                    {
                                        Id = i.Id,
                                        Url = i.Url
                                    }).ToList()
                            },
                            BookingItems = b.BookingItems
                                .Select(bi => new BookingItem
                                {
                                    Id = bi.Id,
                                    AmountItems = bi.AmountItems,
                                    Total = bi.Total,
                                    AmenityService = new AmenityService
                                    {
                                        Id = bi.AmenityService.Id,
                                        Name = bi.AmenityService.Name
                                    }
                                }).ToList()
                        })
                        .ToListAsync();

            return bookingRequests.OrderByDescending(b => b.DateBooking.Add(b.TimeBooking)).ToList();
        }
        public async Task<List<Booking>> ListBookingUserUpComing(Guid userId)
        {
            return await _context.Bookings.Where(b => b.UserId == userId
                                                && b.Status == StatusBookingEnum.Accepted)
                                                .Include(b => b.Room)
                                                    .ThenInclude(r => r.Images)
                                                .ToListAsync();

        }

        public async Task<bool> CancleAllBookingByUser(Guid userId)
        {
            List<Booking> bookings = await _context.Bookings.Where(b => b.UserId == userId && (b.Status == StatusBookingEnum.Wait || b.Status == StatusBookingEnum.Accepted)).ToListAsync();
            foreach (var booking in bookings)
            {
                booking.Status = StatusBookingEnum.Canceled;
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Booking>> GetBookingsWaitAccepted(Guid roomId)
        {
            return await _context.Bookings.Where(b => b.RoomId == roomId
                                            && (b.Status == StatusBookingEnum.Wait
                                            || b.Status == StatusBookingEnum.Accepted))
                                            .Include(b => b.PaymentRefunds
                                                                .Where(p => p.Type == PaymentRefundEnum.Payment))
                                            .ToListAsync();
        }

        public async Task<List<Booking>> GetListBookingByAmenityService(Guid amenityServiceId)
        {
            return await _context.Bookings.Include(b => b.BookingItems
                                            .Where(bi => bi.AmenityServiceId == amenityServiceId))
                                            .Include(b => b.PaymentRefunds
                                                                .Where(p => p.Type == PaymentRefundEnum.Payment))
                                            .Where(b => b.BookingItems.Any(bi => bi.AmenityServiceId == amenityServiceId)
                                            && (b.Status == StatusBookingEnum.Wait || b.Status == StatusBookingEnum.Accepted))
                                            .ToListAsync();
        }

        public async Task<bool> UpdateBookingItem(BookingItem bookingItem)
        {
            _context.BookingItems.Update(bookingItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> TotalBooking()
        {
            return await _context.Bookings.Where(b => b.Status == StatusBookingEnum.Done).CountAsync();
        }

        public async Task<BookingItem?> GetBookingItemById(Guid BookingItemId)
        {
            return await _context.BookingItems
                                        .Include(bi => bi.AmenityService)
                                        .FirstOrDefaultAsync(bi => bi.Id == BookingItemId);
        }

        public async Task<List<Booking>> ScheduleRoom(Guid roomId, DateTime StartDate, DateTime EndDate)
        {
            return await _context.Bookings
                                        .Where(b => b.RoomId == roomId
                                                && (b.Status == StatusBookingEnum.Accepted || b.Status == StatusBookingEnum.Done)
                                                && b.DateBooking >= StartDate
                                                && b.DateBooking < EndDate).ToListAsync();
        }
    }
}