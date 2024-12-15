using BE.src.Domains.DTOs.AmenityService;
using BE.src.Domains.Enum;
using BE.src.Domains.Models;
using BE.src.Repositories;
using BE.src.Shared.Type;
using BE.src.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BE.src.Services
{
    public interface IAmenityServiceServ
    {
        Task<IActionResult> GetAllAmenityService();
        Task<IActionResult> CreateService(CreateServiceDTO data);
        Task<IActionResult> CreateServiceDetail(CreateServiceDetailDTO data);
        Task<IActionResult> UpdateService(Guid id, UpdateServiceDTO service);
        Task<IActionResult> DeleteService(Guid amenityServiceId);
        Task<IActionResult> CheckService(Guid BookingItemsId, Guid StaffId, DeviceCheckingDTO data);
        Task<IActionResult> GetServicesWhenBooking(DateTime startDate, DateTime endDate);
    }

    public class AmenityServiceServ : IAmenityServiceServ
    {
        private readonly IAmenityServiceRepo _amenityServiceRepo;
        private readonly IBookingRepo _bookingRepo;
        private readonly ITransactionRepo _transactionRepo;
        private readonly IUserRepo _userRepo;

        public AmenityServiceServ(IAmenityServiceRepo amenityServiceRepo, IBookingRepo bookingRepo, ITransactionRepo transactionRepo, IUserRepo userRepo)
        {
            _amenityServiceRepo = amenityServiceRepo;
            _bookingRepo = bookingRepo;
            _transactionRepo = transactionRepo;
            _userRepo = userRepo;
        }

        public async Task<IActionResult> CreateService(CreateServiceDTO data)
        {
            try
            {
                var service = new AmenityService()
                {
                    Name = data.Name,
                    Type = data.Type,
                    Price = data.Price,
                    Status = StatusServiceEnum.Available
                };
                if (data.Image == null)
                {
                    return ErrorResp.BadRequest("Image is required");
                }
                string? url = await Utils.UploadImgToFirebase(data.Image, data.Name, "services");
                if (url == null)
                {
                    return ErrorResp.BadRequest("Fail to get url Image");
                }
                var image = new Image()
                {
                    Url = url,
                    AmenityServiceId = service.Id
                };
                bool isCreated = await _amenityServiceRepo.CreateService(service);
                if (!isCreated)
                {
                    return ErrorResp.BadRequest("Fail to create service");
                }
                bool isCreatedImage = await _amenityServiceRepo.CreateServiceImage(image);
                if (!isCreated)
                {
                    return ErrorResp.BadRequest("Fail to create service");
                }
                return SuccessResp.Created("Create service success");

            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> CreateServiceDetail(CreateServiceDetailDTO data)
        {
            try
            {
                ServiceDetail serviceDetail = new()
                {
                    Name = data.Name,
                    IsNormal = true,
                    AmenitySerivceId = data.AmenityServiceId
                };
                bool isCreated = await _amenityServiceRepo.CreateServiceDetail(serviceDetail);
                if (!isCreated)
                {
                    return ErrorResp.BadRequest("Cant create service detail");
                }
                return SuccessResp.Created("Create Service Detail Success");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetAllAmenityService()
        {
            try
            {
                var amenityServices = await _amenityServiceRepo.GetAllAmenityService();
                return SuccessResp.Ok(amenityServices);
            }
            catch (System.Exception)
            {
                return ErrorResp.BadRequest("Error to get list Amenity and Service");
            }
        }

        public async Task<IActionResult> UpdateService(Guid id, UpdateServiceDTO service)
        {
            try
            {
                var serviceToUpdate = await _amenityServiceRepo.GetAmenityServiceById(id);

                if (serviceToUpdate == null)
                {
                    return ErrorResp.BadRequest("Service not found");
                }

                if (service.Name != null)
                {
                    serviceToUpdate.Name = service.Name;
                }
                else
                {
                    serviceToUpdate.Name = serviceToUpdate.Name;
                }

                if (service.Price != null)
                {
                    serviceToUpdate.Price = (float)service.Price;
                }
                else
                {
                    serviceToUpdate.Price = serviceToUpdate.Price;
                }

                serviceToUpdate.UpdateAt = DateTime.Now;

                if (service.Image != null)
                {
                    if (serviceToUpdate.Image == null)
                    {
                        return ErrorResp.BadRequest("Image not found");
                    }
                    string? url = await Utils.UploadImgToFirebase(service.Image, serviceToUpdate.Name, "services");

                    if (url == null)
                    {
                        return ErrorResp.BadRequest("Fail to get url Image");
                    }

                    var image = await _amenityServiceRepo.GetImageByServiceId(id);
                    if (image == null)
                    {
                        return ErrorResp.BadRequest("Image not found");
                    }

                    image.Url = url;
                    image.UpdateAt = DateTime.Now;

                    bool isUpdatedImage = await _amenityServiceRepo.UpdateServiceImage(image);
                    if (!isUpdatedImage)
                    {
                        return ErrorResp.BadRequest("Fail to update image service");
                    }
                }
                else
                {
                    serviceToUpdate.Image = serviceToUpdate.Image;
                }

                bool isUpdated = await _amenityServiceRepo.UpdateService(serviceToUpdate);
                if (!isUpdated)
                {
                    return ErrorResp.BadRequest("Fail to update service");
                }

                return SuccessResp.Ok("Update service success");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }


        public async Task<IActionResult> DeleteService(Guid amenityServiceId)
        {
            try
            {
                //identify booking have this service
                List<Booking> GetListBookingByAmenityService = await _bookingRepo.GetListBookingByAmenityService(amenityServiceId);
                foreach (var booking in GetListBookingByAmenityService)
                {
                    var bookingItem = booking.BookingItems.FirstOrDefault();
                    if (booking.PaymentRefunds.FirstOrDefault()?.PaymentType != PaymentTypeEnum.COD)
                    {
                        // refund
                        PaymentRefund? paymentRefund = await _transactionRepo.FindPaymentRefundByBooking(booking.Id);
                        if (paymentRefund == null)
                        {
                            PaymentRefund newPaymentRefund = new()
                            {
                                Type = PaymentRefundEnum.Refund,
                                Total = bookingItem.Total,
                                PointBonus = 0,
                                Status = true,
                                IsRefundReturnRoom = false
                            };
                            var isCreatePayment = await _transactionRepo.CreatePaymentRefund(newPaymentRefund);
                            paymentRefund = newPaymentRefund;
                        }
                        else
                        {
                            paymentRefund.Total += bookingItem.Total;
                            var isUpdatedPayment = await _transactionRepo.UpdatePaymentRefund(paymentRefund);
                        }
                        RefundItem newRefundItem = new()
                        {
                            AmountItems = bookingItem.AmountItems,
                            Total = bookingItem.Total,
                            PaymentRefundId = paymentRefund.Id,
                            BookingItemId = bookingItem.Id
                        };
                        var isCreatedRefundItem = await _transactionRepo.CreateRefundItem(newRefundItem);
                        //plus amount for wallet
                        var user = await _userRepo.GetUserById(booking.UserId);
                        user.Wallet += bookingItem.Total;
                        var isUpdatedUser = await _userRepo.UpdateUser(user);
                    }
                    // change status bookingservice
                    bookingItem.Status = StatusBookingItemEnum.Cancle;
                    var isUpdateBookingItem = await _bookingRepo.UpdateBookingItem(bookingItem);
                    // send notification
                    Notification notification = new()
                    {
                        Title = "Refunds due as this service is no longer available",
                        Description = $"Booking application on {booking.DateBooking}, has been canceled and an amount of {bookingItem.Total} added to your wallet",
                        UserId = booking.UserId
                    };
                    var isCreateNotification = await _userRepo.CreateNotification(notification);
                }
                //change service status
                var serviceAmenity = await _amenityServiceRepo.GetAmenityServiceById(amenityServiceId);
                serviceAmenity.Status = StatusServiceEnum.Disable;
                var isUpdatedService = await _amenityServiceRepo.UpdateService(serviceAmenity);
                return SuccessResp.Ok("Delete Service Success");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> CheckService(Guid BookingItemsId, Guid StaffId, DeviceCheckingDTO data)
        {
            try
            {
                var checkService = await _amenityServiceRepo.GetDeviceChecking(BookingItemsId);
                if (checkService != null)
                {
                    checkService.StaffId = StaffId;
                    checkService.Status = data.Status;
                    checkService.Description = data.Description;
                    var isUpdated = await _amenityServiceRepo.UpdateDeviceChecking(checkService);
                    if (!isUpdated)
                    {
                        return ErrorResp.BadRequest("Error to update check device");
                    }
                }
                else
                {
                    DeviceChecking deviceChecking = new()
                    {
                        StaffId = StaffId,
                        BookingItemsId = BookingItemsId,
                        Status = data.Status,
                        Description = data.Description
                    };
                    var IsAddded = await _amenityServiceRepo.AddDeviceChecking(deviceChecking);
                    if (!IsAddded)
                    {
                        return ErrorResp.BadRequest("Error to add device check");
                    }
                }
                return SuccessResp.Ok("Check Device Success");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetServicesWhenBooking(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<AmenityService> amenityServicesAvailable = (await _amenityServiceRepo.GetAllAmenityService())
                                                                    .Where(s => s.Status == StatusServiceEnum.Available)
                                                                    .ToList();
                List<GetServicesDTO> returnServices = new();

                foreach (var amenityService in amenityServicesAvailable)
                {
                    GetServicesDTO returnService = new()
                    {
                        amenityService = amenityService
                    };
                    if (amenityService.Type == AmenityServiceTypeEnum.Amenity)
                    {
                        returnService.RemainingQuantity = (await _amenityServiceRepo
                                                            .GetListServiceAvailableByDateAndServiceId(startDate, endDate, amenityService.Id))
                                                            .Count();
                    }
                    returnService.amenityService.ServiceDetails = [];
                    returnServices.Add(returnService);
                }

                return SuccessResp.Ok(returnServices);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }
    }
}