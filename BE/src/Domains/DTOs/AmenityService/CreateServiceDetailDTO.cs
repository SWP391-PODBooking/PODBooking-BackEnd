using BE.src.Domains.Enum;

namespace BE.src.Domains.DTOs.AmenityService
{
    public class CreateServiceDetailDTO
    {
        public required Guid AmenityServiceId { get; set; }
        public required string Name { get; set; }
    }
}