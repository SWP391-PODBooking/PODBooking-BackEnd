using BE.src.Domains.Enum;
using BE.src.Domains.Models.Base;
using System;

namespace BE.src.Domains.Models
{
    public class DeviceChecking : BaseEntity
    {
        public StatusDeviceCheckingEnum Status { get; set; }
        public string Description { get; set; } = null!;

        public Guid BookingItemsId { get; set; }
        public BookingItem BookingItem { get; set; } = null!;

        public Guid? StaffId { get; set; }
        public User? Staff { get; set; } = null!;
    }
}