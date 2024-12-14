using BE.src.Domains.Models.Base;

namespace BE.src.Domains.Models
{
    public class Utility : BaseEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<Room> Rooms { get; set; } = null!;
    }
}