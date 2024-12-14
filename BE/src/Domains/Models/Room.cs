using System.ComponentModel;
using BE.src.Domains.Enum;
using BE.src.Domains.Models.Base;
using Newtonsoft.Json;

namespace BE.src.Domains.Models
{
        public class Room : BaseEntity
        {
                public TypeRoomEnum TypeRoom { get; set; }
                public string Name { get; set; } = null!;
                public float Price { get; set; }
                public StatusRoomEnum Status { get; set; }
                public string Description { get; set; } = null!;

                public Guid AreaId { get; set; }
                public Area Area { get; set; } = null!;
                public ICollection<RatingFeedback> RatingFeedbacks { get; set; } = null!;
                public ICollection<Image> Images { get; set; } = null!;
                public ICollection<Booking> Bookings { get; set; } = null!;
                public ICollection<Favourite> Favourites { get; set; } = null!;
                public ICollection<Utility> Utilities { get; set; } = null!;
        }
}