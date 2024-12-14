using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.src.Domains.Enum;

namespace BE.src.Domains.DTOs
{
    public class RoomDto
    {
        public Guid RoomId { get; set; }
        public TypeRoomEnum TypeRoom { get; set; }
        public List<string> Images { get; set; }
    }
}