using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.src.Domains.Database;
using BE.src.Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace BE.src.Repositories
{
    public interface IReportRepo
    {
        Task<List<RatingFeedback>> GetRatingFeedbacks();
    }

    public class ReportRepo : IReportRepo
    {
        private readonly PodDbContext _context;
        public ReportRepo(PodDbContext context)
        {
            _context = context;
        }

        public async Task<List<RatingFeedback>> GetRatingFeedbacks()
        {
            return await _context.RatingFeedbacks
                                    .Include(rf => rf.User)
                                        .ThenInclude(u => u.Image)
                                    .Include(rf => rf.Room)
                                        .ThenInclude(r => r.Images)
                                    .ToListAsync();
        }
    }
}