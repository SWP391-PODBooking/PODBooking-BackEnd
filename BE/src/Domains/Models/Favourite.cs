using BE.src.Domains.Models.Base;

namespace BE.src.Domains.Models
{
    public class Favourite : BaseEntity
    {
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}