using BE.src.Domains.DTOs.AmenityService;
using BE.src.Domains.DTOs.Booking;
using BE.src.Domains.Enum;
using BE.src.Domains.Models;
using BE.src.Repositories;
using BE.src.Shared.Type;
using BE.src.Util;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using MyNotification = BE.src.Domains.Models.Notification;

namespace BE.src.Services
{
    public interface IBookingServ
    {
        Task<IActionResult> BookingRoom(BookingRoomRqDTO data);
        Task<IActionResult> ViewBookingListOfRoom(Guid roomId);
        Task<IActionResult> AcceptBooking(Guid bookingId);
        Task<IActionResult> CancelBooking(Guid bookingId);
        Task<IActionResult> GetBookingRequests();
        Task<IActionResult> CancleBookingByCustomer(Guid bookingId);
        Task<IActionResult> GetBookingCheckAvailableList(Guid bookingId);
        Task<IActionResult> GetScheduleBookingForStaff(DateTime startDate, DateTime endDate);
        Task<IActionResult> GetBookingRequestsInProgressForStaff();
        Task<IActionResult> ListBookingUserUpcoming(Guid User);
        Task<IActionResult> TotalBooking();
        Task<IActionResult> CancleServiceByCustomer(List<CancleServiceDTO> data, Guid BookingId);
        Task<IActionResult> HandleCheckIn(CheckInRqDTO data);
    }

    public class BookingServ : IBookingServ
    {
        private readonly IBookingRepo _bookingRepo;
        private readonly IUserRepo _userRepo;
        private readonly IAmenityServiceRepo _amenityRepo;
        private readonly IRoomRepo _roomRepo;
        private readonly ITransactionRepo _transactionRepo;

        public BookingServ(IBookingRepo bookingRepo, IUserRepo userRepo, IAmenityServiceRepo amenityRepo, IRoomRepo roomRepo, ITransactionRepo transactionRepo)
        {
            _bookingRepo = bookingRepo;
            _userRepo = userRepo;
            _amenityRepo = amenityRepo;
            _roomRepo = roomRepo;
            _transactionRepo = transactionRepo;
        }

        public async Task<IActionResult> BookingRoom(BookingRoomRqDTO data)
        {
            try
            {
                Booking? CheckBooked = await _bookingRepo.CheckBookedRoom(data.RoomId, data.DateBooking, TimeSpan.FromHours(data.TimeHourBooking));
                if (CheckBooked != null)
                {
                    return ErrorResp.BadRequest("There is another booking placed at the same time");
                }
                Booking? CheckIsRequest = await _bookingRepo.CheckBookReqUser(data.RoomId, data.UserId, data.DateBooking, TimeSpan.FromHours(data.TimeHourBooking));
                if (CheckIsRequest != null)
                {
                    return ErrorResp.BadRequest("You have already request");
                }
                float total = 0;
                Membership? userMembership = await _userRepo.GetMemberShipByUserId(data.UserId);
                Booking booking = new()
                {
                    TimeBooking = TimeSpan.FromHours(data.TimeHourBooking),
                    DateBooking = data.DateBooking,
                    Status = StatusBookingEnum.Wait,
                    UserId = data.UserId,
                    RoomId = data.RoomId,
                    IsPay = false,
                    IsCheckIn = false
                };

                Room? room = await _roomRepo.GetRoomById(data.RoomId);

                if (room == null)
                {
                    return ErrorResp.NotFound("Cant not find room");
                }

                total += room.Price * data.TimeHourBooking;

                bool isCreatedBooking = await _bookingRepo.AddBooking(booking);

                if (!isCreatedBooking)
                {
                    return ErrorResp.BadRequest("Error to create booking");
                }

                if (!(data.BookingItemDTOs == null || data.BookingItemDTOs.Count == 0))
                {
                    List<BookingItem> bookingItems = new List<BookingItem>();
                    foreach (var item in data.BookingItemDTOs)
                    {
                        AmenityService? amenityService = await _amenityRepo.GetAmenityServiceById(item.ItemsId);

                        if (amenityService == null)
                        {
                            return ErrorResp.NotFound("Cant fount this amenity or service");
                        }

                        BookingItem bookingItem = new BookingItem
                        {
                            AmountItems = item.Amount,
                            Total = amenityService.Price * item.Amount,
                            AmenityServiceId = item.ItemsId,
                            Status = StatusBookingItemEnum.Active,
                            BookingId = booking.Id
                        };

                        if (amenityService.Type == AmenityServiceTypeEnum.Amenity)
                        {
                            List<ServiceDetail> serviceDetailAvailable = await _amenityRepo.GetListServiceAvailableByDateAndServiceId(data.DateBooking, data.DateBooking.Add(TimeSpan.FromHours(data.TimeHourBooking)), item.ItemsId);

                            if (serviceDetailAvailable.Any())
                            {
                                Random random = new Random();
                                int randomIndex = random.Next(serviceDetailAvailable.Count);
                                bookingItem.ServiceDetail = serviceDetailAvailable[randomIndex];
                                DeviceChecking deviceChecking = new()
                                {
                                    BookingItemsId = bookingItem.Id,
                                    Status = StatusDeviceCheckingEnum.NonCheck
                                };
                            }
                            else
                            {
                                bookingItem.ServiceDetail = null;
                            }
                        }

                        total += bookingItem.Total;

                        bookingItems.Add(bookingItem);
                    }
                    bool isCreatingBookingItem = await _bookingRepo.AddBookingItems(bookingItems);
                    if (!isCreatingBookingItem)
                    {
                        return ErrorResp.BadRequest("Error to create booking items");
                    }
                }
                if (userMembership != null)
                {
                    total -= room.Price * data.TimeHourBooking * userMembership.Discount;
                }

                booking.Total = total;

                await _bookingRepo.UpdateBooking(booking);

                return SuccessResp.Ok(booking.Id);
            }
            catch (Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> ViewBookingListOfRoom(Guid roomId)
        {
            try
            {
                List<Booking> bookings = await _bookingRepo.ViewBookingOfRoomInFuture(roomId);
                return SuccessResp.Ok(bookings);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> AcceptBooking(Guid bookingId)
        {
            try
            {
                var isAccept = await _bookingRepo.AcceptBooking(bookingId);
                if (isAccept)
                {
                    var isConfirmed = await _bookingRepo.ProcessAcceptBooking(bookingId);
                    if (!isConfirmed)
                    {
                        return ErrorResp.BadRequest("Can not confirm booking");
                    }
                    return SuccessResp.Ok("Accept booking success");
                }
                else
                {
                    return ErrorResp.NotFound("Can not find booking");
                }
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> CancelBooking(Guid bookingId)
        {
            try
            {
                var isDecline = await _bookingRepo.DeclineBooking(bookingId);
                if (isDecline)
                {
                    var isRefunded = await _bookingRepo.ProcessRefund(bookingId);
                    if (isRefunded)
                    {
                        return SuccessResp.Ok("Booking canceled and refund processed successfully.");
                    }
                    else
                    {
                        return ErrorResp.BadRequest("Booking canceled, but no refund applicable.");
                    }
                }
                else
                {
                    return ErrorResp.NotFound("Cannot find booking");
                }
            }
            catch (Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetBookingRequests()
        {
            try
            {
                var bookingRequests = await _bookingRepo.GetBookingRequests();
                if (bookingRequests == null)
                {
                    return ErrorResp.NotFound("Can not find booking requests");
                }
                return SuccessResp.Ok(bookingRequests);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> CancleBookingByCustomer(Guid bookingId)
        {
            try
            {
                var booking = await _bookingRepo.GetBookingWaitOrInProgressById(bookingId);
                if (booking == null)
                {
                    return ErrorResp.BadRequest("Booking being cancle before");
                }
                booking.Status = StatusBookingEnum.Canceled;
                var isUpdated = await _bookingRepo.UpdateBooking(booking);
                if (!isUpdated)
                {
                    return ErrorResp.BadRequest("Cant update booking");
                }
                if (booking.IsPay == true && (booking.PaymentRefunds.FirstOrDefault(p => p.Type == PaymentRefundEnum.Payment).PaymentType == PaymentTypeEnum.Paypal || booking.PaymentRefunds.FirstOrDefault().PaymentType == PaymentTypeEnum.Wallet))
                {
                    Console.WriteLine("cc1");
                    var user = await _userRepo.GetUserById(booking.UserId);
                    if (user == null)
                    {
                        return ErrorResp.BadRequest("Cant find user");
                    }
                    user.Wallet += booking.Total * 0.85f;
                    PaymentRefund paymentRefund = new()
                    {
                        Type = PaymentRefundEnum.Refund,
                        BookingId = bookingId,
                        Total = user.Wallet,
                        PointBonus = 0,
                        Status = true
                    };
                    var isUpdatedUser = await _userRepo.UpdateUser(user);
                    if (!isUpdatedUser)
                    {
                        return ErrorResp.BadRequest("Cant update user");
                    }
                    var isCreatedPayment = await _transactionRepo.CreatePaymentRefund(paymentRefund);
                    if (!isCreatedPayment)
                    {
                        return ErrorResp.BadRequest("Cant create payment refund");
                    }
                    if (paymentRefund.CreateAt == null)
                    {
                        return ErrorResp.BadRequest("Create at is null");
                    }
                    MyNotification notification = new()
                    {
                        Title = "Refund after canceling booking",
                        Description = "Refund of " + paymentRefund.Total + " VND to you on " + Utils.ConvertDateTimeTime(paymentRefund.CreateAt.Value),
                        UserId = user.Id,
                    };
                    var isCreatedNotification = await _userRepo.CreateNotification(notification);
                    if (!isCreatedNotification)
                    {
                        return ErrorResp.BadRequest("Cant Create Notification");
                    }
                }
                return SuccessResp.Ok("Cancle Booking success");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetBookingCheckAvailableList(Guid bookingId)
        {
            try
            {
                var bookingCheckAvailableList = await _bookingRepo.GetBookingCheckAvailableList(bookingId);
                if (bookingCheckAvailableList == null)
                {
                    return ErrorResp.NotFound("Can not find booking check available list");
                }
                return SuccessResp.Ok(bookingCheckAvailableList);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        // var groupedBookings = bookings
        // .GroupBy(b => new DateTime(b.booking.DateBooking.Year, b.booking.DateBooking.Month, b.booking.DateBooking.Day, b.booking.DateBooking.Hour, 0, 0))
        // .Select(group => new BookingScheduleRp
        // {
        //     Amount = group.Count(),
        //     StartBooking = group.Key,
        //     Bookings = group.ToList()
        // })
        // .ToList();

        public async Task<IActionResult> GetScheduleBookingForStaff(DateTime startDate, DateTime endDate)
        {
            try
            {
                var bookings = await _bookingRepo.GetScheduleBookingForStaff(startDate, endDate);

                var groupedBookings = bookings
        .GroupBy(b => new DateTime(b.DateBooking.Year, b.DateBooking.Month, b.DateBooking.Day, b.DateBooking.Hour, 0, 0))
        .Select(group => new BookingScheduleRp
        {
            Amount = group.Count(),
            StartBooking = group.Key,
            Bookings = group.ToList()
        })
        .ToList();
                return SuccessResp.Ok(groupedBookings);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetBookingRequestsInProgressForStaff()
        {
            try
            {
                var bookingRequests = await _bookingRepo.GetBookingRequestsInProgressForStaff();
                if (bookingRequests == null)
                {
                    throw new Exception("Can not find booking requests");
                }
                return SuccessResp.Ok(bookingRequests);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> ListBookingUserUpcoming(Guid UserId)
        {
            try
            {
                var listBooking = await _bookingRepo.ListBookingUserUpComing(UserId);
                return SuccessResp.Ok(listBooking);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> TotalBooking()
        {
            try
            {
                int countBooking = await _bookingRepo.TotalBooking();
                return SuccessResp.Ok(countBooking);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> CancleServiceByCustomer(List<CancleServiceDTO> data, Guid bookingId)
        {
            try
            {
                var booking = await _bookingRepo.GetBookingById(bookingId);
                if (booking == null)
                {
                    return ErrorResp.BadRequest("cant find booking");
                }
                PaymentRefund refund = new()
                {
                    Type = PaymentRefundEnum.Refund,
                    Total = 0,
                    PointBonus = 0,
                    Status = true,
                    IsRefundReturnRoom = false
                };
                var isCreateRefund = await _transactionRepo.CreatePaymentRefund(refund);
                foreach (var cancleServiceDTO in data)
                {
                    //Check and change status CancleSerivce
                    var bookingItem = await _bookingRepo.GetBookingItemById(cancleServiceDTO.BookingItemId);
                    if (bookingItem == null)
                    {
                        return ErrorResp.BadRequest("cant find bookingItem");
                    }
                    if (bookingItem.AmountItems == cancleServiceDTO.Amount)
                    {
                        bookingItem.Status = StatusBookingItemEnum.Cancle;
                    }
                    bookingItem.AmountItems -= cancleServiceDTO.Amount;
                    RefundItem newRefundItem = new()
                    {
                        AmountItems = cancleServiceDTO.Amount,
                        Total = bookingItem.AmenityService.Price * cancleServiceDTO.Amount,
                        PaymentRefundId = refund.Id,
                        BookingItemId = cancleServiceDTO.BookingItemId
                    };
                    var IsCreatedTransaction = await _transactionRepo.CreateRefundItem(newRefundItem);
                    bookingItem.Total -= newRefundItem.Total;
                    var isUpdatedBookingItem = await _bookingRepo.UpdateBookingItem(bookingItem);
                    //Decrease in Booking
                    booking.Total -= newRefundItem.Total;
                    refund.Total += newRefundItem.Total;
                }
                var isUpdatedBooking = await _bookingRepo.UpdateBooking(booking);
                var isUpdateRefund = await _transactionRepo.UpdatePaymentRefund(refund);
                var user = await _userRepo.GetUserById(booking.UserId);
                if (user == null)
                {
                    return ErrorResp.BadRequest("cant find user");
                }
                user.Wallet += refund.Total * 0.85f;
                var isUpdateUser = await _userRepo.UpdateUser(user);
                MyNotification notification = new()
                {
                    Title = "Refund for your Cancle Service",
                    Description = $"Your wallet have been increase {refund.Total * 0.85f} in your waller",
                    UserId = user.Id
                };
                var isCreatedNotification = await _userRepo.CreateNotification(notification);
                //Notification
                return SuccessResp.Ok("Cancle service success");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> HandleCheckIn(CheckInRqDTO data)
        {
            try
            {
                var booking = await _bookingRepo.GetBookingById(data.BookingId);
                if (booking == null)
                {
                    return ErrorResp.NotFound("Cant find Booking");
                }
                if (booking.PaymentRefunds.FirstOrDefault() == null)
                {
                    return ErrorResp.NotFound("Cant find Payment");
                }
                if (booking.PaymentRefunds.FirstOrDefault().PaymentType == PaymentTypeEnum.COD)
                {
                    booking.IsPay = true;
                }
                booking.IsCheckIn = data.IsCheckIn;
                var isUpdatedBooking = await _bookingRepo.UpdateBooking(booking);
                return SuccessResp.Ok("Check In Success");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }
    }
}