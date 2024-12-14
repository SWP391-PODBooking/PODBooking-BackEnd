using BE.src.Domains.Enum;

namespace BE.src.Domains.DTOs.Room
{
    public class BookingsTrendRpDTO
    {
        public required TypeRoomEnum type {get; set;}
        public required List<Models.Room> Rooms {get; set;}
        public required float Total {get; set;}
    }
}