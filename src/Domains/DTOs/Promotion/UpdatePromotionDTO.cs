namespace BE.src.Domains.DTOs.Promotion
{
    public class UpdatePromotionDTO
    {
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal MinimumSpend { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxUsage { get; set; }
        public bool IsActive { get; set; }
    }
} 