using BE.src.Domains.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.src.Repositories
{
    public interface IPromotionRepo
    {
        Task<List<Promotion>> GetAllPromotions();
        Task<Promotion> GetPromotionById(Guid id);
        Task<Promotion> GetPromotionByCode(string code);
        Task<bool> CreatePromotion(Promotion promotion);
        Task<bool> UpdatePromotion(Promotion promotion);
        Task<bool> DeletePromotion(Guid id);
        Task<bool> ValidatePromotion(string code, decimal orderAmount);
    }
} 