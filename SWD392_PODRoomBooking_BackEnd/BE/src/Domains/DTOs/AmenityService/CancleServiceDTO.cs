using PayPal.Api;

namespace BE.src.Domains.DTOs.AmenityService
{
    public class CancleServiceDTO
    {
        public required Guid BookingItemId { get; set; }
        public required int Amount { get; set; }
    }
}