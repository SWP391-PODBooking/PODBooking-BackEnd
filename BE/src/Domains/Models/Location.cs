using BE.src.Domains.Models.Base;

namespace BE.src.Domains.Models
{
        public class Location : BaseEntity
        {
                public string Address { get; set; } = null!;
                public float Longitude { get; set; }
                public float Latitude { get; set; }

                public Area Area { get; set; } = null!;
        }
}