using BE.src.Domains.Enum;
using BE.src.Domains.Models.Base;
using System;

namespace BE.src.Domains.Models
{
        public class Booking : BaseEntity
        {
                public TimeSpan TimeBooking { get; set; }
                public DateTime DateBooking { get; set; }
                public float Total { get; set; }
                public StatusBookingEnum Status { get; set; }
                public bool IsPay { get; set; }
                public bool IsCheckIn { get; set; }

                public Guid UserId { get; set; }
                public User User { get; set; } = null!;
                public Guid RoomId { get; set; }
                public Room Room { get; set; } = null!;
                public Guid? MembershipUserId { get; set; }
                public MembershipUser? MembershipUser { get; set; } = null!;
                public ICollection<BookingItem> BookingItems { get; set; } = null!;
                public ICollection<PaymentRefund> PaymentRefunds { get; set; } = null!;
        }
}