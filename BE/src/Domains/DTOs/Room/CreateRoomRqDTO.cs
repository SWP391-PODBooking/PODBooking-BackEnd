using BE.src.Domains.Enum;
using BE.src.Domains.Models;

namespace BE.src.Domains.DTOs.Room
{
    public class CreateRoomRqDTO
    {
        public required Guid AreaId { get; set; }
        public required TypeRoomEnum RoomType { get; set; }
        public required string Name { get; set; }
        public required float Price { get; set; }
        public required string Description { get; set; }
        public required List<Guid> UtilitiesId { get; set; }
        public required List<IFormFile> Images { get; set; }
    }
}
