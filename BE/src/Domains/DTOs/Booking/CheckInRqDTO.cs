namespace BE.src.Domains.DTOs.Booking
{
    public class CheckInRqDTO
    {
        public required Guid BookingId { get; set; }
        public required bool IsCheckIn { get; set; }
    }
}