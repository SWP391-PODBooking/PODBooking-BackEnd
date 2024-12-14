using BE.src.Domains.Enum;
using BE.src.Domains.Models.Base;

namespace BE.src.Domains.Models
{
        public class AmenityService : BaseEntity
        {
                public string Name { get; set; } = null!;
                public AmenityServiceTypeEnum Type { get; set; }
                public float Price { get; set; }
                public StatusServiceEnum Status { get; set; }

                public ICollection<BookingItem> BookingItems { get; set; } = null!;
                public Image Image { get; set; } = null!;
                public ICollection<ServiceDetail> ServiceDetails { get; set; } = null!;
        }
}