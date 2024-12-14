namespace BE.src.Domains.DTOs.Transaction
{
    public class StatisticMonth
    {
        public required int Month { get; set; }
        public required float Amount { get; set; }
        public required float Refund { get; set; }
    }
}