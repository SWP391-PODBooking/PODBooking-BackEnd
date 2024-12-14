using BE.src.Domains.Models.Base;

namespace BE.src.Domains.Models
{
    public class Image : BaseEntity
    {
        public string Url { get; set; } = null!;
        public Guid? RoomId { get; set; }
        public Room? Room { get; set; } = null!;

        public Guid? AreaId { get; set; }
        public Area? Area { get; set; } = null!;

        public Guid? UserId { get; set; }
        public User? User { get; set; } = null!;

        public Guid? AmenityServiceId {get; set;}
        public AmenityService? AmenityService {get; set;} = null!;
    }
}