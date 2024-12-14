using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.src.Domains.Models;
using BE.src.Repositories;
using BE.src.Shared.Type;
using Microsoft.AspNetCore.Mvc;

namespace BE.src.Services
{
    public interface IReportServ
    {
        Task<IActionResult> GetRatingFeedbacks();
    }

    public class ReportServ : IReportServ
    {
        private readonly IReportRepo _reportRepo;
        public ReportServ(IReportRepo reportRepo)
        {
            _reportRepo = reportRepo;
        }

        public async Task<IActionResult> GetRatingFeedbacks()
        {
            try
            {
                var ratingFeedbacks = await _reportRepo.GetRatingFeedbacks();
                if (ratingFeedbacks == null)
                {
                    return ErrorResp.NotFound("Not found rating feedbacks");
                }
                else
                {
                    return SuccessResp.Ok(ratingFeedbacks);
                }
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }
    }
}