using BE.src.Domains.Enum;
using BE.src.Domains.Models;
using BE.src.Domains.Models.Base;

namespace BE.src.Domains.DTOs.Transaction
{
    public class AmenityServiceDTO {
        public string Name { get; set; } = null!;
        public float Price { get; set; }
    }
    public class BookingItemDTO: BaseEntity {
        public int AmountItems { get; set; }
        public float Total { get; set; }
        public StatusBookingItemEnum Status { get; set; }
        public Guid AmenityServiceId { get; set; }
        public AmenityServiceDTO AmenityService { get; set; } = null!;
        public DeviceChecking DeviceChecking { get; set; } = null!;
    }
    public class TransactionDTO : BaseEntity
    {
        public TypeTransactionEnum TransactionType { get; set; }
        public float Total { get; set; }

        public Guid? PaymentRefundId { get; set; }
        public PaymentRefund? PaymentRefund { get; set; }

        public Guid? MembershipUserId { get; set; }
        public MembershipUser? MembershipUser { get; set; }

        public Guid? DepositWithdrawId { get; set; }
        public DepositWithdraw? DepositWithdraw { get; set; }

        public Guid UserId { get; set; }

        public List<BookingItemDTO> BookingItemDTOList { get; set; }
    }
}
