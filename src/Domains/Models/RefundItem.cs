using BE.src.Domains.Models.Base;

namespace BE.src.Domains.Models
{
    public class RefundItem : BaseEntity
    {
        public int AmountItems { get; set; }
        public float Total { get; set; }

        public Guid PaymentRefundId { get; set; }
        public PaymentRefund PaymentRefund { get; set; } = null!;
        public Guid BookingItemId { get; set; }
        public BookingItem BookingItem { get; set; } = null!;
    }
}
