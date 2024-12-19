using Microsoft.AspNetCore.Mvc;
using BE.src.Domains.DTOs.Promotion;
using BE.src.Repositories;
using BE.src.Domains.Models;

namespace BE.src.Services
{
    public class PromotionServ : IPromotionServ
    {
        private readonly IPromotionRepo _promotionRepo;

        public PromotionServ(IPromotionRepo promotionRepo)
        {
            _promotionRepo = promotionRepo;
        }

        public async Task<IActionResult> GetAllPromotions()
        {
            try
            {
                var promotions = await _promotionRepo.GetAllPromotions();
                return new OkObjectResult(new
                {
                    Status = 200,
                    Message = "Get all promotions successfully",
                    Data = promotions
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    Status = 400,
                    Message = ex.Message
                });
            }
        }

        public async Task<IActionResult> GetPromotionById(Guid id)
        {
            try
            {
                var promotion = await _promotionRepo.GetPromotionById(id);
                if (promotion == null)
                    return new NotFoundObjectResult(new
                    {
                        Status = 404,
                        Message = "Promotion not found"
                    });

                return new OkObjectResult(new
                {
                    Status = 200,
                    Message = "Get promotion successfully",
                    Data = promotion
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    Status = 400,
                    Message = ex.Message
                });
            }
        }

        public async Task<IActionResult> CreatePromotion(CreatePromotionDTO dto)
        {
            try
            {
                var existingPromotion = await _promotionRepo.GetPromotionByCode(dto.Code);
                if (existingPromotion != null)
                    return new BadRequestObjectResult(new
                    {
                        Status = 400,
                        Message = "Promotion code already exists"
                    });

                var promotion = new Promotion
                {
                    Id = Guid.NewGuid(),
                    Code = dto.Code,
                    Description = dto.Description,
                    DiscountAmount = dto.DiscountAmount,
                    MinimumSpend = dto.MinimumSpend,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    MaxUsage = dto.MaxUsage,
                    CurrentUsage = 0
                };

                var result = await _promotionRepo.CreatePromotion(promotion);
                if (!result)
                    return new BadRequestObjectResult(new
                    {
                        Status = 400,
                        Message = "Create promotion failed"
                    });

                return new OkObjectResult(new
                {
                    Status = 200,
                    Message = "Create promotion successfully",
                    Data = promotion
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    Status = 400,
                    Message = ex.Message
                });
            }
        }

        public async Task<IActionResult> UpdatePromotion(Guid id, UpdatePromotionDTO dto)
        {
            try
            {
                var promotion = await _promotionRepo.GetPromotionById(id);
                if (promotion == null)
                    return new NotFoundObjectResult(new
                    {
                        Status = 404,
                        Message = "Promotion not found"
                    });

                promotion.Description = dto.Description;
                promotion.DiscountAmount = dto.DiscountAmount;
                promotion.MinimumSpend = dto.MinimumSpend;
                promotion.StartDate = dto.StartDate;
                promotion.EndDate = dto.EndDate;
                promotion.MaxUsage = dto.MaxUsage;
                promotion.IsActive = dto.IsActive;

                var result = await _promotionRepo.UpdatePromotion(promotion);
                if (!result)
                    return new BadRequestObjectResult(new
                    {
                        Status = 400,
                        Message = "Update promotion failed"
                    });

                return new OkObjectResult(new
                {
                    Status = 200,
                    Message = "Update promotion successfully",
                    Data = promotion
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    Status = 400,
                    Message = ex.Message
                });
            }
        }

        public async Task<IActionResult> DeletePromotion(Guid id)
        {
            try
            {
                var result = await _promotionRepo.DeletePromotion(id);
                if (!result)
                    return new NotFoundObjectResult(new
                    {
                        Status = 404,
                        Message = "Promotion not found or delete failed"
                    });

                return new OkObjectResult(new
                {
                    Status = 200,
                    Message = "Delete promotion successfully"
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    Status = 400,
                    Message = ex.Message
                });
            }
        }

        public async Task<IActionResult> ValidatePromotion(string code, decimal orderAmount)
        {
            try
            {
                var promotion = await _promotionRepo.GetPromotionByCode(code);
                
                if (promotion == null)
                {
                    return new BadRequestObjectResult(new
                    {
                        Status = 400,
                        Message = "Promotion code not found"
                    });
                }

                if (!promotion.IsActive)
                {
                    return new BadRequestObjectResult(new
                    {
                        Status = 400,
                        Message = "Promotion code is inactive"
                    });
                }

                if (DateTime.Now < promotion.StartDate)
                {
                    return new BadRequestObjectResult(new
                    {
                        Status = 400,
                        Message = "Promotion has not started yet"
                    });
                }

                if (DateTime.Now > promotion.EndDate)
                {
                    return new BadRequestObjectResult(new
                    {
                        Status = 400,
                        Message = "Promotion has expired"
                    });
                }

                if (promotion.CurrentUsage >= promotion.MaxUsage)
                {
                    return new BadRequestObjectResult(new
                    {
                        Status = 400,
                        Message = "Promotion usage limit reached"
                    });
                }

                if (orderAmount < promotion.MinimumSpend)
                {
                    return new BadRequestObjectResult(new
                    {
                        Status = 400,
                        Message = $"Minimum spend required: {promotion.MinimumSpend}"
                    });
                }

                return new OkObjectResult(new
                {
                    Status = 200,
                    Message = "Promotion code is valid",
                    Data = new
                    {
                        discountAmount = promotion.DiscountAmount,
                        minimumSpend = promotion.MinimumSpend,
                        code = promotion.Code,
                        description = promotion.Description
                    }
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    Status = 400,
                    Message = "Error validating promotion",
                    Error = ex.Message
                });
            }
        }
    }
} 