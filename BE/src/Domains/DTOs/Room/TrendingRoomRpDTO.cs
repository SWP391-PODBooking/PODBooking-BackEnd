using BE.src.Domains.Enum;

namespace BE.src.Domains.DTOs.Room
{
    public class TrendingRoomRpDTO
    {
        public required TypeRoomEnum Type { get; set; }
        public required float Amount { get; set; }
        public required int BookingsCount { get; set; }
    }
}