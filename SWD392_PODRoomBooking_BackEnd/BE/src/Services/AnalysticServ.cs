using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.src.Repositories;
using BE.src.Shared.Type;
using Microsoft.AspNetCore.Mvc;

namespace BE.src.Services
{
    public interface IAnalysticServ
    {
        Task<IActionResult> GetMonthlyRevenue();
        Task<IActionResult> GetNumberOfAccountRegistered();
        Task<IActionResult> GetMostPurchasedRooms();
    }
    public class AnalysticServ : IAnalysticServ
    {
        private readonly IAnalysticRepo _analysticRepo;
        public AnalysticServ(IAnalysticRepo analysticRepo)
        {
            _analysticRepo = analysticRepo;
        }

        public async Task<IActionResult> GetMonthlyRevenue()
        {
            try
            {
                var monthlyRevenue = await _analysticRepo.GetMonthlyRevenue();
                if(monthlyRevenue == null)
                {
                    return ErrorResp.NotFound("Monthly revenue not found");
                }
                return SuccessResp.Ok(monthlyRevenue);
            }
            catch (Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetMostPurchasedRooms()
        {
            try
            {
                var mostPurchasedRooms = await _analysticRepo.GetMostPurchasedRooms();
                if(mostPurchasedRooms == null)
                {
                    return ErrorResp.NotFound("Most purchased rooms not found");
                }
                return SuccessResp.Ok(mostPurchasedRooms);
            }
            catch (Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetNumberOfAccountRegistered()
        {
            try
            {
                var numberOfAccountRegistered = await _analysticRepo.GetNumberOfAccountRegistered();
                if(numberOfAccountRegistered == null)
                {
                    return ErrorResp.NotFound("Number of account registered not found");
                }
                return SuccessResp.Ok(numberOfAccountRegistered);
            }
            catch (Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }
    }
}