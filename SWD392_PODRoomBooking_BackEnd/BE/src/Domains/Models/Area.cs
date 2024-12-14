using BE.src.Domains.Models.Base;

namespace BE.src.Domains.Models
{
    public class Area : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public Guid LocationId { get; set; }
        public Location Location { get; set; } = null!;
        public ICollection<Image> Images { get; set; } = null!;
        public ICollection<Room> Rooms { get; set; } = null!;
        public ICollection<UserAreaManagement> UserAreaManagements { get; set; } = null!;
    }                    
}