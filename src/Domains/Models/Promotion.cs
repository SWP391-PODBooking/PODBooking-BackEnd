public class Promotion
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal MinimumSpend { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MaxUsage { get; set; }
    public int CurrentUsage { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
} 