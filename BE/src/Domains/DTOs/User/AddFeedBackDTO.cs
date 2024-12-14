namespace BE.src.Domains.DTOs.User
{
    public class AddFeedBackDTO
    {
        public required string Feedback { get; set; }
        public required int RatingStar { get; set; }
    }
}