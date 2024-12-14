namespace BE.src.Domains.DTOs.Booking
{
    public class BookingScheduleRp
    {
        public required int Amount { get; set; }
        public required DateTime StartBooking { get; set; }
        public required List<Models.Booking> Bookings { get; set; }
    }
}