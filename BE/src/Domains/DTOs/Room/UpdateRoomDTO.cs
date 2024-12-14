using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.src.Domains.Enum;

namespace BE.src.Domains.DTOs.Room
{
    public class UpdateRoomDTO
    {
        public TypeRoomEnum? RoomType { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }
        public string? Description { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}