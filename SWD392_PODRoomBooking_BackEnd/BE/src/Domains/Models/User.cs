using BE.src.Domains.Enum;
using System.ComponentModel.DataAnnotations;
using BE.src.Domains.Models.Base;
using Newtonsoft.Json;

namespace BE.src.Domains.Models
{
        public class User : BaseEntity
        {
                public string? Name { get; set; } = null!;
                public string? Phone { get; set; } = null!;
                public string Email { get; set; } = null!;
                public string? Password { get; set; } = null!;
                public string? Username { get; set; } = null!;
                public DateTime? DOB { get; set; }
                public float Wallet { get; set; }
                public UserStatusEnum Status { get; set; }

                public Guid RoleId { get; set; }
                public Role Role { get; set; } = null!;
                public ICollection<MembershipUser> MembershipUsers { get; set; } = null!;
                public Image Image { get; set; } = null!;
                public ICollection<Notification> Notifications { get; set; } = null!;
                public ICollection<Transaction> Transactions { get; set; } = null!;
                public ICollection<Favourite> Favourites { get; set; } = null!;
                public ICollection<UserAreaManagement> UserAreaManagements { get; set; } = null!;
                public ICollection<RatingFeedback> RatingFeedbacks { get; set; } = null!;
                [JsonIgnore]
                public ICollection<Booking> Bookings { get; set; } = null!;
                public ICollection<DepositWithdraw> DepositWithdraws { get; set; } = null!;
                public ICollection<DeviceChecking> DeviceCheckings { get; set; } = null!;
        }
}