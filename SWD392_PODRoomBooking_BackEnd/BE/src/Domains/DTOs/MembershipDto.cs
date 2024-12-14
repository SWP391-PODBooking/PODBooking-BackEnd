using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.src.Domains.DTOs
{
    public class MembershipDto
    {
        public string Name { get; set; }
        public int Rank { get; set; }
        public float Discount { get; set; }
        public float Price { get; set; }
    }
}