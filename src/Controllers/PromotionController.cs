using Microsoft.AspNetCore.Mvc;
using BE.src.Services;
using BE.src.Domains.DTOs.Promotion;

namespace BE.src.Controllers
{
    [ApiController]
    [Route("promotion")]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionServ _promotionServ;

        public PromotionController(IPromotionServ promotionServ)
        {
            _promotionServ = promotionServ;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPromotions()
        {
            return await _promotionServ.GetAllPromotions();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPromotionById(Guid id)
        {
            return await _promotionServ.GetPromotionById(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePromotion([FromBody] CreatePromotionDTO dto)
        {
            return await _promotionServ.CreatePromotion(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePromotion(Guid id, [FromBody] UpdatePromotionDTO dto)
        {
            return await _promotionServ.UpdatePromotion(id, dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(Guid id)
        {
            return await _promotionServ.DeletePromotion(id);
        }

        [HttpGet("validate")]
        public async Task<IActionResult> ValidatePromotion([FromQuery] string code, [FromQuery] decimal orderAmount)
        {
            return await _promotionServ.ValidatePromotion(code, orderAmount);
        }
    }
}