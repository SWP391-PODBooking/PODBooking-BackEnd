using Org.BouncyCastle.Asn1.Mozilla;

namespace BE.src.Domains.DTOs.Booking
{
    public class BookingRoomRqDTO
    {
        public required Guid RoomId { get; set; }
        public required Guid UserId { get; set; }
        public List<BookingItemDTO>? BookingItemDTOs { get; set; }
        public required int TimeHourBooking { get; set; }
        public required DateTime DateBooking { get; set; }
    }
}
