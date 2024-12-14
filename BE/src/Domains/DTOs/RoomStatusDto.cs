using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.src.Domains.Enum;

namespace BE.src.Domains.DTOs
{
    public class RoomStatusDto
    {
        public Guid RoomId { get; set; }
        public StatusRoomEnum IsRented { get; set; }
        public TimeSpan StartDay { get; set; }
        public TimeSpan EndDay { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}