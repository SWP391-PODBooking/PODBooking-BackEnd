using BE.src.Domains.DTOs.AmenityService;
using BE.src.Domains.Models;
using BE.src.Services;
using Microsoft.AspNetCore.Mvc;

namespace BE.src.Controllers
{
    [ApiController]
    [Route("amenityservice/")]

    public class AmenityServiceController : ControllerBase
    {
        private readonly IAmenityServiceServ _amenityServiceServ;

        public AmenityServiceController(IAmenityServiceServ amenityServiceServ)
        {
            _amenityServiceServ = amenityServiceServ;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAmenityService()
        {
            return await _amenityServiceServ.GetAllAmenityService();
        }

        [HttpPost("CreateService")]
        public async Task<IActionResult> CreateService([FromForm] CreateServiceDTO data)
        {
            return await _amenityServiceServ.CreateService(data);
        }
        [HttpPost("CreateServiceDetail")]
        public async Task<IActionResult> CreateServiceDetail([FromBody] CreateServiceDetailDTO data)
        {
            return await _amenityServiceServ.CreateServiceDetail(data);
        }

        [HttpPut("UpdateService/{id}")]
        public async Task<IActionResult> UpdateService(Guid id, [FromForm] UpdateServiceDTO service)
        {
            return await _amenityServiceServ.UpdateService(id, service);
        }

        [HttpPut("DeleteService/{amenityServiceId}")]
        public async Task<IActionResult> DeleteService(Guid amenityServiceId)
        {
            return await _amenityServiceServ.DeleteService(amenityServiceId);
        }

        [HttpPost("CheckService")]
        public async Task<IActionResult> CheckService([FromQuery] Guid BookingItemsId, [FromQuery] Guid StaffId, [FromBody] DeviceCheckingDTO data)
        {
            return await _amenityServiceServ.CheckService(BookingItemsId, StaffId, data);
        }
    }
}