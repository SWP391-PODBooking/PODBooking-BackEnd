namespace BE.src.Domains.DTOs.User
{
    public class ResetPassRqDTO
    {
        public required Guid UserId { get; set; }
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
