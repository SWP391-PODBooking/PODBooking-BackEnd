using BE.src.Domains.Enum;
using BE.src.Domains.Models.Base;
using System;

namespace BE.src.Domains.Models
{
    public class BookingItem : BaseEntity
    {
        public int AmountItems { get; set; }
        public float Total { get; set; }
        public StatusBookingItemEnum Status { get; set; }

        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = null!;
        public Guid AmenityServiceId { get; set; }
        public AmenityService AmenityService { get; set; } = null!;
        public Guid? ServiceDetailId { get; set; }
        public ServiceDetail? ServiceDetail { get; set; } = null!;
        public ICollection<RefundItem> RefundItems { get; set; } = null!;
        public DeviceChecking DeviceChecking { get; set; } = null!;
    }
}