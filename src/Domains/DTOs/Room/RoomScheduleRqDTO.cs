namespace BE.src.Domains.DTOs.Room
{
    public class RoomScheduleRqDTO
    {
        public required Guid RoomId { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
    }
}