using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.src.Services;
using Microsoft.AspNetCore.Mvc;

namespace BE.src.Controllers
{
    [ApiController]
    [Route("analysis/")]
    public class AnalysticController : ControllerBase
    {
        private readonly IAnalysticServ _analysticServ;
        public AnalysticController(IAnalysticServ analysticServ)
        {
            _analysticServ = analysticServ;
        }
        [HttpGet("monthly-revenue")]
        public async Task<IActionResult> GetMonthlyRevenue()
        {
            return await _analysticServ.GetMonthlyRevenue();
        }
    }
}