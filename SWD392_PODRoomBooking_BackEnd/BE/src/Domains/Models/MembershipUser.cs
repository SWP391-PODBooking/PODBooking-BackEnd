using BE.src.Domains.Models.Base;
using System.Transactions;

namespace BE.src.Domains.Models
{
        public class MembershipUser : BaseEntity
        {
                public bool Status { get; set; }
                public Guid MembershipId { get; set; }
                public Membership Membership { get; set; } = null!;

                public Guid UserId { get; set; }
                public User User { get; set; } = null!;

                public Transaction Transaction { get; set; } = null!;
                public ICollection<Booking> Bookings { get; set; } = null!;
        }
}