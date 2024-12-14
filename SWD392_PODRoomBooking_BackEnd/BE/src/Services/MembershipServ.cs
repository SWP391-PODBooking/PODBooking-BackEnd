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
using 

namespace BE.src.Services
{
    public interface IMembershipServ
    {
        Task<IActionResult> GetMembershipDetailsAsync(Guid membershipId);
        Task<IActionResult> CreateMembership(MembershipCreateDTO data);
        Task<IActionResult> GetAllMembership();
        Task<IActionResult> UpdateMembership(Guid id, MembershipUpdateDTO data);
        Task<IActionResult> DeleteMembership(Guid id);
        //Task<IActionResult> GetUserMembership(Guid userId, Guid membershipId);
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
            var membershipMapping = new Dictionary<string, (string Name, decimal Discount)>
            {
                { "Bronze", ("Bronze", 0.05M) },
                { "Silver", ("Silver", 0.10M) },
                { "Gold", ("Gold", 0.15M) }
            };
            if (!membershipMapping.TryGetValue(membership.Name, out var membershipInfo))
            {
                return new NotFoundObjectResult(new { Message = "Invalid membership type" });
            }
            var timeSpan = DateTime.UtcNow - membership.CreateAt.Value;
            var totalDays = timeSpan.TotalDays;
            var timeLeft = membership.TimeLeft - timeSpan.Days;

            // Trả về thông tin Membership
            return new OkObjectResult(new
            {
                MembershipType = membershipInfo.Name,
                Discount = membershipInfo.Discount,
                TimeLeft = timeLeft > 0 ? timeLeft : 0,
                Price = membership.Price,
                Rank = membership.Rank

            });
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

        //public async Task<IActionResult> GetUserMembership(Guid userId, Guid membershipId)
        //{
        //    try
        //    {
        //        return SuccessResp.Ok(await _membershipRepo.GetUserMembership());
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return ErrorResp.BadRequest(ex.Message);
        //    }
        //}
    }
}