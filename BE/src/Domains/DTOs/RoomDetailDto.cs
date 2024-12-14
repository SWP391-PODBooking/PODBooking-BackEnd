using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.src.Domains.DTOs
{
    public class RoomDetailDto
    {
        public Guid RoomId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
        public List<string> Images { get; set; }
    }
}