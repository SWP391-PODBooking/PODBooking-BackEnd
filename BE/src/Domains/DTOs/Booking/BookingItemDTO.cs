using System.ComponentModel;

namespace BE.src.Domains.DTOs.Booking
{
    public class BookingItemDTO
    {
        public required Guid ItemsId { get; set; }
        public required int Amount {  get; set; }
    }
}
