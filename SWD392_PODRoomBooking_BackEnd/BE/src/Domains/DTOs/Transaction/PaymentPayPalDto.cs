namespace BE.src.Domains.DTOs.Transaction
{
    public class PaymentPayPalDto
    {
        public required Guid BookingId { get; set; }
    }
}