using BE.src.Domains.Enum;
using BE.src.Domains.Models.Base;
using System;
using System.Transactions;

namespace BE.src.Domains.Models
{
    public class PaymentRefund : BaseEntity
    {
        public PaymentRefundEnum Type { get; set; }
        public float Total { get; set; }
        public int PointBonus { get; set; }
        public PaymentTypeEnum? PaymentType { get; set; }
        public bool Status { get; set; }
        public bool? IsRefundReturnRoom { get; set; }

        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public ICollection<RefundItem> RefundItems { get; set; } = null!;
        public Transaction Transaction { get; set; } = null!;
    }
}