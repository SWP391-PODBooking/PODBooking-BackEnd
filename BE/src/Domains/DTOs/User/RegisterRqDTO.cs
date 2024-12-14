namespace BE.src.Domains.DTOs.User
{
    public class RegisterRqDTO
    {
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }
    }
}
