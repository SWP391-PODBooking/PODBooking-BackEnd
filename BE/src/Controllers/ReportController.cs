using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.src.Services;
using Microsoft.AspNetCore.Mvc;

namespace BE.src.Controllers
{
    [ApiController]
    [Route("report/")]
    public class ReportController : ControllerBase
    {
        private readonly IReportServ _reportServ;
        public ReportController(IReportServ reportServ)
        {
            _reportServ = reportServ;
        }

        [HttpGet("rating-feedbacks")]
        public async Task<IActionResult> GetRatingFeedbacks()
        {
            return await _reportServ.GetRatingFeedbacks();
        }
    }
}