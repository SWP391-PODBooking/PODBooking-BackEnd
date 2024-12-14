namespace BE.src.Domains.DTOs.Room
{
    public class RoomAnalysticDTO
    {
        public required Models.Room Room { get; set; }
        public required int CountBooking { get; set; }
        public required float TotalRevenue { get; set; }
    }
}