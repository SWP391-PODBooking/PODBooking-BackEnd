using BE.src.Domains.DTOs;
using BE.src.Domains.Models;
using BE.src.Repositories;
using BE.src.Shared.Type;
using Microsoft.AspNetCore.Mvc;
using BE.src.Util;
using BE.src.Domains.Enum;
using BE.src.Domains.DTOs.Room;

namespace BE.src.Services
{
    public interface IRoomServ
    {
        // Search and filter room
        Task<IActionResult> GetRoomBySearchInput(string inputInfo);
        Task<IActionResult> GetRoomByFilterTypeRoom(TypeRoomEnum typeRoom);
        Task<IActionResult> GetRoomListWithBookingTimes(Guid? areaId, TypeRoomEnum? typeRoom, DateTime? startDate, DateTime? endDate);

        // Return room detail
        Task<IActionResult> ViewRoomDetail(Guid roomId);
        Task<IActionResult> ViewRoomsByArea(Guid areaId);

        Task<IActionResult> CreateRoom(CreateRoomRqDTO data);
        Task<IActionResult> ViewRoomDetail(string hashCode);
        Task<IActionResult> GetCommentByRoomId(Guid roomId);
        Task<IActionResult> ViewListFavourite(Guid userId);
        Task<IActionResult> UnOrFavouriteRoom(Guid roomId, Guid userId);
        Task<IActionResult> GetScheduleRoom(RoomScheduleRqDTO data);
        Task<IActionResult> TrendingRoom();
        Task<IActionResult> DeleteRoom(Guid RoomId);
        Task<IActionResult> UpdateRoom(Guid id, UpdateRoomDTO data);
        Task<IActionResult> RoomSchedule(Guid roomId, DateTime StartDate, DateTime EndDate);
    }
    public class RoomServ : IRoomServ
    {
        private readonly IRoomRepo _roomRepo;
        private readonly IAreaRepo _areaRepo;
        private readonly IBookingRepo _bookingRepo;
        private readonly ITransactionRepo _transactionRepo;
        private readonly IUserRepo _userRepo;

        public RoomServ(IRoomRepo roomRepo, IAreaRepo areaRepo, IBookingRepo bookingRepo, ITransactionRepo transactionRepo, IUserRepo userRepo)
        {
            _roomRepo = roomRepo;
            _areaRepo = areaRepo;
            _bookingRepo = bookingRepo;
            _transactionRepo = transactionRepo;
            _userRepo = userRepo;
        }

        public async Task<IActionResult> GetRoomBySearchInput(string inputInfo)
        {
            var room = await _roomRepo.SearchRoomByInput(inputInfo);
            if (room == null)
            {
                return ErrorResp.NotFound("Not found room");
            }
            else
            {
                return SuccessResp.Ok(room);
            }
        }

        public async Task<IActionResult> GetRoomByFilterTypeRoom(TypeRoomEnum typeRoom)
        {
            var room = await _roomRepo.FilterRoomByTypeRoom(typeRoom);
            if (room == null)
            {
                return ErrorResp.NotFound("Not found room");
            }
            else
            {
                return SuccessResp.Ok(room);
            }
        }

        public async Task<IActionResult> ViewRoomDetail(Guid roomId)
        {
            var roomDetail = await _roomRepo.GetRoomDetailsById(roomId);

            if (roomDetail == null)
            {
                return ErrorResp.NotFound("Not found room");
            }
            else
            {
                return SuccessResp.Ok(roomDetail);
            }
        }

        public async Task<IActionResult> ViewRoomsByArea(Guid areaId)
        {
            var rooms = await _roomRepo.GetRoomsByAreaId(areaId);

            if (rooms == null)
            {
                return ErrorResp.NotFound("Not found room");
            }
            else
            {
                return SuccessResp.Ok(rooms);
            }
        }

        public async Task<IActionResult> CreateRoom(CreateRoomRqDTO data)
        {
            try
            {
                Area? area = await _areaRepo.GetAreaById(data.AreaId);
                List<Utility> utilities = await _roomRepo.GetListUtilitiesById(data.UtilitiesId);
                if (area == null)
                {
                    return ErrorResp.BadRequest("Error getting area");
                }
                var room = new Room
                {
                    TypeRoom = data.RoomType,
                    Name = data.Name,
                    Price = data.Price,
                    Status = StatusRoomEnum.Available,
                    Description = data.Description,
                    AreaId = data.AreaId,
                    Utilities = utilities
                };
                var isCreatedRoom = await _roomRepo.CreateRoom(room);
                if (!isCreatedRoom)
                {
                    return ErrorResp.BadRequest("Error Creating room");
                }
                else
                {
                    int count = 0;
                    foreach (IFormFile image in data.Images)
                    {
                        string? urlFirebase = await Utils.UploadImgToFirebase(image, count.ToString(), $"Room/{Utils.ConvertToUnderscore(area.Name)}/{Utils.ConvertToUnderscore(data.Name)}");
                        if (urlFirebase == null)
                        {
                            return ErrorResp.BadRequest("Fail to save image to firebase");
                        }
                        var imageObj = new Image
                        {
                            Room = room,
                            Url = urlFirebase
                        };
                        var isImageCreated = await _roomRepo.AddImageRoom(imageObj);
                        if (!isImageCreated)
                        {
                            return ErrorResp.BadRequest("Fail to save image to database");
                        }
                        count++;
                    }
                    return SuccessResp.Created("Create room success");
                }
            }
            catch (Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> ViewRoomDetail(string hashCode)
        {
            try
            {
                var room = await _roomRepo.GetRoomDetailByHashCode(hashCode);
                if (room == null)
                {
                    return ErrorResp.NotFound("Cant found the room");
                }
                int favouriteCount = await _roomRepo.GetCountFavouriteRoom(room.Id);
                RoomDetailRpDTO returnRoom = new()
                {
                    Info = room,
                    FavouriteCount = favouriteCount
                };
                return SuccessResp.Ok(returnRoom);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetCommentByRoomId(Guid roomId)
        {
            try
            {
                List<RatingFeedback> ratingFeedbacks = await _roomRepo.GetListRatingFeedback(roomId);
                return SuccessResp.Ok(ratingFeedbacks);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> ViewListFavourite(Guid userId)
        {
            try
            {
                List<Room> rooms = await _roomRepo.GetListFavouriteRoom(userId);
                return SuccessResp.Ok(rooms);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> UnOrFavouriteRoom(Guid roomId, Guid userId)
        {
            try
            {
                Favourite? favouriteRoom = await _roomRepo.GetFavouriteRoomByUser(roomId, userId);
                if (favouriteRoom != null)
                {
                    bool isDelete = await _roomRepo.DeleteFavouriteRoom(favouriteRoom);
                    if (isDelete)
                    {
                        return SuccessResp.Ok("Remove favourite success");
                    }
                    else
                    {
                        return ErrorResp.BadRequest("Fail to remove favourite");
                    }
                }
                else
                {
                    Favourite newFavouriteRoom = new()
                    {
                        RoomId = roomId,
                        UserId = userId
                    };
                    bool isAdd = await _roomRepo.AddFavouriteRoom(newFavouriteRoom);
                    if (isAdd)
                    {
                        return SuccessResp.Ok("Add favourite success");
                    }
                    else
                    {
                        return ErrorResp.BadRequest("Fail to add favourite");
                    }
                }
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetScheduleRoom(RoomScheduleRqDTO data)
        {
            try
            {
                List<RoomScheduleRpDTO> roomSchedules = new List<RoomScheduleRpDTO>();
                List<Booking> availableBookings = await _bookingRepo.ViewBookingAvailablePeriod(data.RoomId, data.StartDate, data.EndDate);

                return SuccessResp.Ok(roomSchedules);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> TrendingRoom()
        {
            try
            {
                List<TrendingRoomRpDTO> returnValues = new List<TrendingRoomRpDTO>();
                foreach (TypeRoomEnum roomType in Enum.GetValues(typeof(TypeRoomEnum)))
                {
                    List<RoomAnalysticDTO> rooms = await _roomRepo.TrendingRoom(roomType);
                    float totalComeIn = 0;
                    int TotalBooking = 0;
                    foreach (var room in rooms)
                    {
                        totalComeIn += room.TotalRevenue;
                        TotalBooking += room.CountBooking;
                    }

                    TrendingRoomRpDTO returnValue = new()
                    {
                        Type = roomType,
                        Amount = totalComeIn,
                        BookingsCount = TotalBooking
                    };
                    returnValues.Add(returnValue);
                }
                returnValues = returnValues.OrderByDescending(o => o.BookingsCount).ToList();
                return SuccessResp.Ok(returnValues);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetRoomListWithBookingTimes(Guid? areaId, TypeRoomEnum? typeRoom, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                List<Room> rooms = await _roomRepo.GetRoomListWithBookingTimes(areaId, typeRoom, startDate, endDate);
                return SuccessResp.Ok(rooms);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteRoom(Guid roomId)
        {
            try
            {
                Console.WriteLine("cc");
                List<Booking> bookingsWaitAccepted = await _bookingRepo.GetBookingsWaitAccepted(roomId);
                Console.WriteLine("a");
                foreach (var booking in bookingsWaitAccepted)
                {
                    //Check Cod
                    if (booking.PaymentRefunds.FirstOrDefault()?.PaymentType != PaymentTypeEnum.COD)
                    {
                        Console.WriteLine("b");
                        //Refund for user
                        PaymentRefund newPaymentRefund = new()
                        {
                            Type = PaymentRefundEnum.Refund,
                            Total = booking.Total,
                            PointBonus = 0,
                            Status = true,
                            BookingId = booking.Id
                        };

                        var isCreateRefund = await _transactionRepo.CreatePaymentRefund(newPaymentRefund);
                        //Add Transaction
                        Console.WriteLine("c");
                        Transaction transaction = new()
                        {
                            TransactionType = TypeTransactionEnum.Refund,
                            Total = booking.Total,
                            PaymentRefundId = newPaymentRefund.Id,
                            UserId = booking.UserId
                        };
                        var isCreateTransaction = await _transactionRepo.CreateTransaction(transaction);
                        Console.WriteLine("c");
                        //Add Money for customer
                        var user = await _userRepo.GetUserById(booking.UserId);
                        Console.WriteLine("c");
                        if (user == null)
                        {
                            return ErrorResp.NotFound("Cant find user");
                        }
                        user.Wallet += booking.Total;
                        var isUpdateUser = await _userRepo.UpdateUser(user);
                        Console.WriteLine("c");
                    }
                    //Send Notification
                    Notification notification = new()
                    {
                        Title = "Refunds due as this room is no longer available",
                        Description = $"Booking application on {booking.DateBooking}, has been canceled and an amount of {booking.Total} added to your wallet",
                        UserId = booking.UserId
                    };
                    var isCreateNotification = await _userRepo.CreateNotification(notification);
                    Console.WriteLine("c");
                    //Change Booking Status
                    booking.Status = StatusBookingEnum.Canceled;
                    var isUpdateBooking = await _bookingRepo.UpdateBooking(booking);
                    Console.WriteLine("c");
                }
                //Change Room Status
                var room = await _roomRepo.GetRoomById(roomId);
                if (room == null)
                {
                    return ErrorResp.BadRequest("Cant find Room");
                }
                room.Status = StatusRoomEnum.Disable;
                var isUpdateRoom = await _roomRepo.UpdateRoom(room);
                return SuccessResp.Ok("Delete Room Successfull");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> UpdateRoom(Guid id, UpdateRoomDTO data)
        {
            try
            {
                var room = await _roomRepo.GetRoomById(id);
                if (room == null)
                {
                    return ErrorResp.NotFound("Not found room");
                }

                room.TypeRoom = data.RoomType ?? room.TypeRoom;
                room.Name = data.Name ?? room.Name;
                room.Price = data.Price ?? room.Price;
                room.Description = data.Description ?? room.Description;

                var existingImages = await _roomRepo.GetImagesByRoomId(id);

                Console.WriteLine(existingImages.Count + " - " + data.Images.Count);

                if (existingImages == null || existingImages.Count == 0)
                {
                    return ErrorResp.NotFound("Not found image");
                }

                if (data.Images == null || data.Images.Count == 0)
                {
                    room.Images = existingImages;

                    var roomUpdated = await _roomRepo.UpdateSecondImageRoom(existingImages);
                    if (!roomUpdated)
                    {
                        return ErrorResp.BadRequest("Error updating room");
                    }
                }
                else
                {
                    int count = 0;
                    foreach (IFormFile image in data.Images)
                    {
                        string? urlFirebase = await Utils.UploadImgToFirebase(image, count.ToString(),
                                $"Room/{Utils.ConvertToUnderscore(room.Area?.Name ?? "Unknown")}/{Utils.ConvertToUnderscore(room.Name)}");
                        if (urlFirebase == null)
                        {
                            return ErrorResp.BadRequest("Fail to save image to firebase");
                        }

                        var imageRoom = await _roomRepo.GetImagesByRoomIdTpUpdate(id);
                        if (imageRoom == null)
                        {
                            return ErrorResp.NotFound("Not found image");
                        }

                        var imagesToKeep = new List<Image>();

                        foreach (var img in imageRoom)
                        {
                            var imageObj = new Image
                            {
                                Id = img.Id,
                                Url = img.Url,
                                UpdateAt = DateTime.Now
                            };

                            var isImageUpdated = await _roomRepo.UpdateImageRoom(imageObj);
                            if (!isImageUpdated)
                            {
                                return ErrorResp.BadRequest("Fail to save image to database");
                            }

                            imagesToKeep.Add(imageObj);
                        }

                        var imgFirstUpdated = await _roomRepo.GetImageBySingleRoomId(id);

                        var allImages = await _roomRepo.GetImagesByRoomId(id);
                        var imagesToDelete = allImages
                                        .Where(img => imgFirstUpdated?.UpdateAt != null &&
                                                        imgFirstUpdated.UpdateAt.Value.Ticks < img.UpdateAt.Value.Ticks)
                                        .ToList();

                        foreach (var img in imagesToDelete)
                        {
                            var isImageDeleted = await _roomRepo.DeleteImageRoom(img.Id);
                            if (!isImageDeleted)
                            {
                                return ErrorResp.BadRequest("Fail to delete image from database");
                            }
                        }

                        count++;
                    }
                }              

                room.UpdateAt = DateTime.Now;

                var isUpdated = await _roomRepo.UpdateRoom(room);
                if (!isUpdated)
                {
                    return ErrorResp.BadRequest("Error updating room");
                }

                return SuccessResp.Ok("Update room success");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> RoomSchedule(Guid roomId, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                List<Booking> bookings = await _bookingRepo.ScheduleRoom(roomId, StartDate, EndDate);
                return SuccessResp.Ok(bookings);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }
    }
}