using BE.src.Domains.Models.Base;

namespace BE.src.Domains.Models
{
    public class Membership : BaseEntity 
    {
        public string Name { get; set; } = null!;
        public float Discount { get; set; }
        public int TimeLeft { get; set; }
        public float Price { get; set; }
        public int Rank { get; set; }

        public ICollection<MembershipUser> MembershipUsers { get; set; } = null!;
    }
}