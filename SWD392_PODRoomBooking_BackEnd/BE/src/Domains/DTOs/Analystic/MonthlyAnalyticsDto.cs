using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.src.Domains.DTOs.Analystic
{
    public class MonthlyAnalyticsDto
    {
        public string Month { get; set; } = string.Empty;
        public float? Revenue { get; set; }        
    }
}