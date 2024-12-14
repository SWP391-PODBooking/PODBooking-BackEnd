using BE.src.Domains.Enum;

namespace BE.src.Domains.DTOs.AmenityService
{
    public class DeviceCheckingDTO
    {
        public required StatusDeviceCheckingEnum Status { get; set; }
        public required string Description { get; set; }

    }
}