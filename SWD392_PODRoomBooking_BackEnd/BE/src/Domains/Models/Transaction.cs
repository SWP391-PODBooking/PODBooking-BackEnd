using BE.src.Domains.Enum;
using BE.src.Domains.Models.Base;
using System;

namespace BE.src.Domains.Models
{
    public class Transaction : BaseEntity
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
        public User User { get; set; } = null!;
    }
}
