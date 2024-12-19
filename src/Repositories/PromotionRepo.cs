using Microsoft.EntityFrameworkCore;
using BE.src.Domains.Database;
using BE.src.Domains.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.src.Repositories
{
    public class PromotionRepo : IPromotionRepo
    {
        private readonly PodDbContext _context;

        public PromotionRepo(PodDbContext context)
        {
            _context = context;
        }

        public async Task<List<Promotion>> GetAllPromotions()
        {
            return await _context.Promotions.ToListAsync();
        }

        public async Task<Promotion> GetPromotionById(Guid id)
        {
            return await _context.Promotions.FindAsync(id);
        }

        public async Task<Promotion> GetPromotionByCode(string code)
        {
            return await _context.Promotions.FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<bool> CreatePromotion(Promotion promotion)
        {
            try
            {
                promotion.CreatedAt = DateTime.Now;
                promotion.IsActive = true;
                _context.Promotions.Add(promotion);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdatePromotion(Promotion promotion)
        {
            try
            {
                promotion.UpdatedAt = DateTime.Now;
                _context.Promotions.Update(promotion);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeletePromotion(Guid id)
        {
            try
            {
                var promotion = await _context.Promotions.FindAsync(id);
                if (promotion != null)
                {
                    promotion.IsActive = false;
                    promotion.UpdatedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ValidatePromotion(string code, decimal orderAmount)
        {
            var promotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Code == code);
            
            if (promotion == null || !promotion.IsActive)
                return false;

            if (DateTime.Now < promotion.StartDate || DateTime.Now > promotion.EndDate)
                return false;

            if (promotion.CurrentUsage >= promotion.MaxUsage)
                return false;

            if (orderAmount < promotion.MinimumSpend)
                return false;

            return true;
        }
    }
}