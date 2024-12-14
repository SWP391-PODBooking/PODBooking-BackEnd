using BE.src.Domains.Models.Base;

namespace BE.src.Domains.Models
{
    public class ServiceDetail : BaseEntity
    {
        public string Name { get; set; } = null!;
        public bool IsNormal { get; set; }

        public Guid AmenitySerivceId { get; set; }
        public AmenityService AmenityService { get; set; } = null!;

        public ICollection<BookingItem> BookingItems { get; set; } = null!;
    }
}