using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.src.Domains.DTOs.AmenityService
{
    public class UpdateServiceDTO
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public IFormFile Image { get; set; }
    }
}