namespace BE.src.Domains.DTOs.Transaction{
    public class MembershipCreateDTO{
        public required string Name {get; set;}
        public required float Discount {get; set;}
        public required int DayLeft {get; set;}
        public required float Price {get; set;}
        public required int Rank {get; set;}
    }
}