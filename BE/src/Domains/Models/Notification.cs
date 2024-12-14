using BE.src.Domains.Models.Base;

namespace BE.src.Domains.Models
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}