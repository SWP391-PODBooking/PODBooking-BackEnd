using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BE.src.Domains.DTOs.User
{
    public class UpdateProfileDTO
    {
        public required Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Username { get; set; }
        public required DateTime DOB { get; set; }
        public required IFormFile Image { get; set; }
    }
}