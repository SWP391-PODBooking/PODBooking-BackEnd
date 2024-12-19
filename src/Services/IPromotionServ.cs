using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BE.src.Domains.DTOs.Promotion;

namespace BE.src.Services
{
    public interface IPromotionServ
    {
        Task<IActionResult> GetAllPromotions();
        Task<IActionResult> GetPromotionById(Guid id);
        Task<IActionResult> CreatePromotion(CreatePromotionDTO dto);
        Task<IActionResult> UpdatePromotion(Guid id, UpdatePromotionDTO dto);
        Task<IActionResult> DeletePromotion(Guid id);
        Task<IActionResult> ValidatePromotion(string code, decimal orderAmount);
    }
} 