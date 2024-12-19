namespace BE.src.Domains.DTOs.Promotion
{
    public class CreatePromotionDTO
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal MinimumSpend { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxUsage { get; set; }
    }
} 