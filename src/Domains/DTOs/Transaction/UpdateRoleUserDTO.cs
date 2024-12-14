using BE.src.Domains.Enum;
namespace BE.src.Domains.DTOs.User
{
    public class UpdateRoleUserDTO
    {
        public RoleEnum? roles { get; set; }
        public UserStatusEnum? status { get; set; }
    }
}