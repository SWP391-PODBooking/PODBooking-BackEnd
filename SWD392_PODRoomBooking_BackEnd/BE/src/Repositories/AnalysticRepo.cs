using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BE.src.Domains.Database;
using BE.src.Domains.DTOs.Analystic;
using BE.src.Domains.Enum;
using BE.src.Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace BE.src.Repositories
{
    public interface IAnalysticRepo
    {
        Task<List<MonthlyAnalyticsDto>> GetMonthlyRevenue();
        Task<int?> GetNumberOfAccountRegistered();
        Task<List<Room>> GetMostPurchasedRooms();
    }

    public class AnalysticRepo : IAnalysticRepo
    {
        private readonly PodDbContext _context;
        public AnalysticRepo(PodDbContext context)
        {
            _context = context;
        }

        public async Task<List<MonthlyAnalyticsDto>> GetMonthlyRevenue()
        {
            var monthlyData = new List<MonthlyAnalyticsDto>();

            for (int month = 1; month <= 12; month++)
            {
                var startDate = new DateTime(DateTime.UtcNow.Year, month, 1);
                var endDate = startDate.AddMonths(1).AddTicks(-1);

                var revenue = await _context.Transactions.Where(t => t.CreateAt >= startDate && t.CreateAt <= endDate)
                                            .Select(t => t.Total)
                                            .SumAsync();

                monthlyData.Add(new MonthlyAnalyticsDto
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
                    Revenue = revenue
                });
            }

            return monthlyData;
        }

        public async Task<int?> GetNumberOfAccountRegistered()
        {
            var accountCount = await _context.Roles
                                    .Where(role => role.Name.Equals("Customer"))
                                    .CountAsync();
            return accountCount;
        }

        public async Task<List<Room>> GetMostPurchasedRooms()
        {
            var mostPurchasedRoomIds = await _context.Bookings
                                        .Where(b => b.Status == StatusBookingEnum.Done)
                                        .Include(b => b.Room)
                                            .ThenInclude(r => r.Images)
                                        .GroupBy(b => b.RoomId)
                                        .OrderByDescending(g => g.Count())
                                        .Select(g => g.Key)
                                        .Take(10)
                                        .ToListAsync();

            var rooms = await _context.Rooms
                .Where(r => mostPurchasedRoomIds.Contains(r.Id))
                .ToListAsync();

            return rooms;
        }
    }
}