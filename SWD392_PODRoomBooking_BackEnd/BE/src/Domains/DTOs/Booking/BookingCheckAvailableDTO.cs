using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE.src.Domains.Enum;

namespace BE.src.Domains.DTOs.Booking
{
    public class BookingCheckAvailableDTO
    {
        public Guid BookingId { get; set; }
        public TimeSpan TimeBooking { get; set; }
        public DateTime DateBooking { get; set; }
        public float Total { get; set; }
        public StatusBookingEnum Status { get; set; }
        public bool IsPay { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}