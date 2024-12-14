using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.src.Domains.DTOs.Transaction;
using BE.src.Services;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace BE.src.Controllers
{
    [ApiController]
    [Route("membership/")]
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipServ _membershipServ;
        public MembershipController(IMembershipServ membershipServ)
        {
            _membershipServ = membershipServ;
        }

        [HttpGet("get-membership-details")]
        public async Task<IActionResult> GetMembershipDetails(Guid id)
        {
            //var result = await _membershipServ.GetMembershipDetailsAsync(id);
            //if (result is NotFoundResult) { return NotFound(new {Message = "Membership package not buy yet! Please buy once."})}
            //if (result is ObjectResult objectResult)
            //{
            //    var membershipData = objectResult.Value as dynamic;
            //    if (membershipData?.MembershipId)
            //}
                
            return await _membershipServ.GetMembershipDetailsAsync(id);
        }
        [HttpPost("Create-membership")]
        public async Task<IActionResult> CreateMembership([FromBody] MembershipCreateDTO data)
        {
            return await _membershipServ.CreateMembership(data);
        }

        [HttpGet("Get-All")]
        public async Task<IActionResult> GetAllMembership()
        {
            return await _membershipServ.GetAllMembership();
        }

        [HttpPut("Update-membership/{id}")]
        public async Task<IActionResult> UpdateMembership(Guid id, [FromBody] MembershipUpdateDTO data)
        {
            return await _membershipServ.UpdateMembership(id, data);
        }

        [HttpDelete("Delete-membership/{id}")]
        public async Task<IActionResult> DeleteMembership(Guid id)
        {
            return await _membershipServ.DeleteMembership(id);
        }

        //[HttpGet("Get-user-membership/{id}")]
        //public async Task<IActionResult> GetUserMembership(Guid userId, Guid membershipId)
        //{
        //    return await _membershipServ.GetUserMembership(userId, membershipId);
        //}

        //[HttpGet("Get-all-user-membership/")]
        //public async Task<IActionResult> GetAllUserMembership()
        //{
        //    return await _membershipServ.GetAllUserMembership();
        //}

    }
}