using BE.src.Domains.Enum;
using BE.src.Domains.Models.Base;

namespace BE.src.Domains.Models
{
        public class Role : BaseEntity
        {
                public RoleEnum Name { get; set; }

                public ICollection<User> Users { get; set; } = null!;
        }
}