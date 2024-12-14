using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.src.Domains.DTOs.Area
{
    public class UpdateAreaDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}