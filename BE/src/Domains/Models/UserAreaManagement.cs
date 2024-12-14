using BE.src.Domains.Models.Base;

namespace BE.src.Domains.Models
{
    public class UserAreaManagement : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid AreaId { get; set; }
        public Area Area { get; set; } = null!;
    }
}