using BE.src.Domains.Models.Base;

namespace BE.src.Domains.Models
{
    public class RatingFeedback : BaseEntity 
    {
        public string Feedback { get; set; } = null!;
        public int RatingStar { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;
    }
}