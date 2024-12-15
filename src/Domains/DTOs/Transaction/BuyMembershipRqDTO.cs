namespace BE.src.Domains.DTOs.Transaction
{
    public class BuyMembershipRqDTO
    {
        public required Guid UserId { get; set; }
        public required Guid MembershipId { get; set; }
    }
}