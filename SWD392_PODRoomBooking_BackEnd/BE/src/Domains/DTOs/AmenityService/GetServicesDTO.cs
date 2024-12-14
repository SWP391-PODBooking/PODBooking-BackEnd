using BE.src.Domains.Enum;

namespace BE.src.Domains.DTOs.AmenityService
{
    public class GetServicesDTO
    {
        public required Models.AmenityService amenityService;
        public required int RemainingQuantity;
    }
}