using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.src.Domains.DTOs.Transaction;
using BE.src.Domains.Models;
using BE.src.Repositories;
using BE.src.Shared.Type;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BE.src.Services
{
    public interface IMembershipServ
    {
        Task<IActionResult> GetMembershipDetailsAsync(Guid membershipId);
        Task<IActionResult> CreateMembership(MembershipCreateDTO data);
        Task<IActionResult> GetAllMembership();
        Task<IActionResult> UpdateMembership(Guid id, MembershipUpdateDTO data);
        Task<IActionResult> DeleteMembership(Guid id);
    }

    public class MembershipServ : IMembershipServ
    {
        private readonly IMembershipRepo _membershipRepo;
        public MembershipServ(IMembershipRepo membershipRepo)
        {
            _membershipRepo = membershipRepo;
        }
        public async Task<IActionResult> GetMembershipDetailsAsync(Guid membershipId)
        {
            var membership = await _membershipRepo.GetMembershipDetails(membershipId);

            if (membership == null)
            {
                return new NotFoundObjectResult(new { Message = "Membership not found" });
            }

            return new OkObjectResult(membership);
        }
        public async Task<IActionResult> CreateMembership(MembershipCreateDTO data)
        {
            try
            {
                Membership membership = new()
                {
                    Name = data.Name,
                    Discount = data.Discount,
                    TimeLeft = data.DayLeft,
                    Price = data.Price,
                    Rank = data.Rank
                };
                bool isCreated = await _membershipRepo.CreateMembership(membership);
                if (!isCreated)
                {
                    return ErrorResp.BadRequest("Fail to create membership");
                }
                return SuccessResp.Created("Created membership");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetAllMembership()
        {
            try
            {
                return SuccessResp.Ok(await _membershipRepo.GetAllMembership());
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> UpdateMembership(Guid id, MembershipUpdateDTO data)
        {
            Membership membership = new()
            {
                Name = data.Name,
                Discount = data.Discount,
                TimeLeft = data.DayLeft,
                Price = data.Price,
                Rank = data.Rank
            };
            try
            {
                var membershipInfo = await _membershipRepo.GetMembershipDetails(id);
                if (membershipInfo == null)
                {
                    return ErrorResp.BadRequest("Membership not found");
                }

                var isUpdated = await _membershipRepo.UpdateMembership(membership);
                if (!isUpdated)
                {
                    return ErrorResp.BadRequest("Fail to update membership");
                }
                return SuccessResp.Ok("Updated membership");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteMembership(Guid id)
        {
            try
            {
                var isDeleted = await _membershipRepo.DeleteMembership(id);
                if (!isDeleted)
                {
                    return ErrorResp.BadRequest("Fail to delete membership");
                }
                return SuccessResp.Ok("Deleted membership");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }
    }
}