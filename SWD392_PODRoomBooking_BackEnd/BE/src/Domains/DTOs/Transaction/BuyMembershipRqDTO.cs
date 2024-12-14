namespace BE.src.Domains.DTOs.Transaction
{
    public class BuyMembershipRqDTO
    {
        public required Guid UserId;
        public required Guid MembershipId;
    }
}