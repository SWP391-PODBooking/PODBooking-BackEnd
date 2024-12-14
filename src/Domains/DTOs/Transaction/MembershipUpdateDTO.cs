using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.src.Domains.DTOs.Transaction
{
    public class MembershipUpdateDTO
    {
        public required string Name {get; set;}
        public required float Discount {get; set;}
        public required int DayLeft {get; set;}
        public required float Price {get; set;}
        public required int Rank {get; set;}
    }
}