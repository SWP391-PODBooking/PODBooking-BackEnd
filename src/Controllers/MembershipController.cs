using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.src.Domains.DTOs.Transaction;
using BE.src.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}