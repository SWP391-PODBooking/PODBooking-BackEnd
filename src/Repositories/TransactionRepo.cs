using BE.src.Domains.Models;
using BE.src.Domains.Database;
using Microsoft.EntityFrameworkCore;

namespace BE.src.Repositories
{
    public interface ITransactionRepo
    {
        Task<List<Transaction>> GetTransactions(Guid userId);
        Task<bool> CreatePaymentRefund(PaymentRefund paymentRefund);
        Task<bool> CreateTransaction(Transaction transaction);
        Task<List<Transaction>> TransactionInYear(int year);
        Task<Membership?> GetMembership(Guid id);
        Task<PaymentRefund?> FindPaymentRefundByBooking(Guid bookingId);
        Task<bool> CreateRefundItem(RefundItem refundItem);
        Task<bool> UpdatePaymentRefund(PaymentRefund paymentRefund);
        Task<float> TotalIncome();
    }

    public class TrasactionRepo : ITransactionRepo
    {
        private readonly PodDbContext _context;

        public TrasactionRepo(PodDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePaymentRefund(PaymentRefund paymentRefund)
        {
            _context.PaymentRefunds.Add(paymentRefund);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateRefundItem(RefundItem refundItem)
        {
            _context.RefundItems.Add(refundItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PaymentRefund?> FindPaymentRefundByBooking(Guid bookingId)
        {
            return await _context.PaymentRefunds.FirstOrDefaultAsync(p => p.BookingId == bookingId);
        }

        public async Task<Membership?> GetMembership(Guid id)
        {
            return await _context.Memberships.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Transaction>> GetTransactions(Guid userId)
        {
            var query = _context.Transactions
                                .Where(t => t.UserId == userId)
                                .Include(t => t.MembershipUser)
                                .Include(t => t.DepositWithdraw)
                                .AsQueryable();

            query = query.Include(t => t.PaymentRefund)
                         .ThenInclude(t => t.Booking)
                         .ThenInclude(t => t.BookingItems);

            var transactions = await query.ToListAsync();

            foreach (var transaction in transactions)
            {
                if (transaction.PaymentRefund != null && transaction.PaymentRefund.Type == Domains.Enum.PaymentRefundEnum.Refund)
                {
                    await _context.Entry(transaction.PaymentRefund)
                                  .Collection(t => t.RefundItems)
                                  .LoadAsync();
                }
            }

            return transactions;
        }

        public async Task<List<Transaction>> TransactionInYear(int year)
        {
            return await _context.Transactions.Where(t => t.CreateAt.HasValue
                                            && t.CreateAt.Value.Year == year
                                            && t.DepositWithdraw == null).ToListAsync();
        }

        public async Task<bool> UpdatePaymentRefund(PaymentRefund paymentRefund)
        {
            _context.PaymentRefunds.Update(paymentRefund);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<float> TotalIncome()
        {
            return await _context.Transactions
                                 .Include(t => t.PaymentRefund)
                                 .Where(t => t.DepositWithdrawId == null
                                             && (t.PaymentRefund == null || t.PaymentRefund.Type != Domains.Enum.PaymentRefundEnum.Refund))
                                 .SumAsync(t => t.Total);
        }
    }
}