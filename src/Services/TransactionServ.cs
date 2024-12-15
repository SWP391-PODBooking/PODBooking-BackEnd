using BE.src.Domains.DTOs.Transaction;
using MyTransaction = BE.src.Domains.Models.Transaction;
using BE.src.Repositories;
using BE.src.Shared.Constant;
using BE.src.Shared.Type;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using BE.src.Domains.Models;
using PaypalTransaction = PayPal.Api.Transaction;
using BE.src.Domains.Enum;

namespace BE.src.Services
{
    public interface ITransactionServ
    {
        Task<IActionResult> TransactionHistory(Guid userId);
        Task<IActionResult> PaymentByPaypal(PaymentPayPalDto data);
        Task<IActionResult> PaymentPaypalSuccess(Guid bookingId);
        Task<IActionResult> PaymentByCod(Guid bookingId);
        Task<IActionResult> StatisticMonthInYear(int year);
        Task<IActionResult> BuyMembership(BuyMembershipRqDTO data);
        Task<IActionResult> PaymentMembershipSuccess(Guid MembershipId, Guid UserId);
        Task<IActionResult> TotalIncome();
    }

    public class TrasactionServ : ITransactionServ
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly IBookingRepo _bookingRepo;
        private readonly IUserRepo _userRepo;
        public TrasactionServ(ITransactionRepo transactionRepo, IBookingRepo bookingRepo, IUserRepo userRepo)
        {
            _transactionRepo = transactionRepo;
            _bookingRepo = bookingRepo;
            _userRepo = userRepo;
        }

        private APIContext GetAPIContext()
        {
            var clientId = Paypal.ClientId;
            var clientSecret = Paypal.Secret;
            var accessToken = new OAuthTokenCredential(clientId, clientSecret).GetAccessToken();
            return new APIContext(accessToken) { Config = new Dictionary<string, string> { { "mode", Paypal.Mod } } };
        }

        private Payment CreatePayment(float total, string returnUrl, string cancelUrl)
        {
            var apiContext = GetAPIContext();

            decimal newTotal = Math.Round((decimal)total / 24850, 2);
            Console.WriteLine(newTotal);

            var payment = new Payment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal" },
                redirect_urls = new RedirectUrls
                {
                    cancel_url = cancelUrl,
                    return_url = returnUrl
                },
                transactions = new List<PaypalTransaction>
                {
                    new PaypalTransaction
                    {
                        description = "Transaction description",
                        invoice_number = Guid.NewGuid().ToString(),
                        amount = new Amount
                        {
                            currency = "USD",
                            total = newTotal.ToString("F2")
                        }
                    }
                }
            };

            return payment.Create(apiContext);
        }

        public async Task<IActionResult> PaymentByPaypal(PaymentPayPalDto data)
        {
            try
            {
                var booking = await _bookingRepo.GetBookingById(data.BookingId);
                if (booking == null)
                {
                    return ErrorResp.NotFound("Cant find booking");
                }
                string return_url = $"http://localhost:5101/transaction/Payment-PayPal-Success?bookingId={data.BookingId}";
                string cancel_url = "http://localhost:5173/";
                var payment = CreatePayment(booking.Total, return_url, cancel_url);
                var approvalUrl = payment.links.FirstOrDefault(lnk => lnk.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;
                return SuccessResp.Ok(approvalUrl);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> TransactionHistory(Guid userId)
        {
            try
            {
                List<MyTransaction> transactions = await _transactionRepo.GetTransactions(userId);
                return SuccessResp.Ok(transactions);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> PaymentPaypalSuccess(Guid bookingId)
        {
            try
            {
                var booking = await _bookingRepo.GetBookingById(bookingId);
                if (booking == null)
                {
                    return ErrorResp.NotFound("Cant find booking");
                }
                booking.IsPay = true;
                bool isUpdateBooking = await _bookingRepo.UpdateBooking(booking);
                if (!isUpdateBooking)
                {
                    return ErrorResp.BadRequest("Cant update booking");
                }
                PaymentRefund payment = new()
                {
                    Type = PaymentRefundEnum.Payment,
                    Total = booking.Total,
                    PointBonus = 0,
                    BookingId = bookingId,
                    PaymentType = PaymentTypeEnum.Paypal,
                    Status = true
                };
                bool isCreatedPayment = await _transactionRepo.CreatePaymentRefund(payment);
                if (!isCreatedPayment)
                {
                    return ErrorResp.BadRequest("Cant create payment");
                }
                MyTransaction transaction = new()
                {
                    TransactionType = TypeTransactionEnum.Payment,
                    Total = booking.Total,
                    PaymentRefund = payment,
                    UserId = booking.UserId
                };
                bool isCreatedTrasaction = await _transactionRepo.CreateTransaction(transaction);
                if (!isCreatedTrasaction)
                {
                    return ErrorResp.BadRequest("Cant create transaction");
                }
                return SuccessResp.Redirect("http://localhost:5173/");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> PaymentByCod(Guid bookingId)
        {
            try
            {
                var booking = await _bookingRepo.GetBookingById(bookingId);
                if (booking == null)
                {
                    return ErrorResp.NotFound("Cant find booking");
                }
                booking.IsPay = false;
                bool isUpdateBooking = await _bookingRepo.UpdateBooking(booking);
                if (!isUpdateBooking)
                {
                    return ErrorResp.BadRequest("Cant update booking");
                }
                PaymentRefund payment = new()
                {
                    Type = PaymentRefundEnum.Payment,
                    Total = booking.Total,
                    PointBonus = 0,
                    BookingId = bookingId,
                    PaymentType = PaymentTypeEnum.COD,
                    Status = false
                };
                bool isCreatedPayment = await _transactionRepo.CreatePaymentRefund(payment);
                if (!isCreatedPayment)
                {
                    return ErrorResp.BadRequest("Cant create payment");
                }
                MyTransaction transaction = new()
                {
                    TransactionType = TypeTransactionEnum.Payment,
                    Total = booking.Total,
                    PaymentRefund = payment,
                    UserId = booking.UserId
                };
                bool isCreatedTrasaction = await _transactionRepo.CreateTransaction(transaction);
                if (!isCreatedTrasaction)
                {
                    return ErrorResp.BadRequest("Cant create transaction");
                }
                return SuccessResp.Ok("Payment cod done");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> StatisticMonthInYear(int year)
        {
            try
            {
                var transactions = await _transactionRepo.TransactionInYear(year);
                List<StatisticMonth> returnValue = new(){
                    new StatisticMonth(){
                        Month = 1,
                        Amount = 0,
                        Refund = 0
                    },
                    new StatisticMonth(){
                        Month = 2,
                        Amount = 0,
                        Refund = 0
                    },
                    new StatisticMonth(){
                        Month = 3,
                        Amount = 0,
                        Refund = 0
                    },
                    new StatisticMonth(){
                        Month = 4,
                        Amount = 0,
                        Refund = 0
                    },
                    new StatisticMonth(){
                        Month = 5,
                        Amount = 0,
                        Refund = 0
                    },
                    new StatisticMonth(){
                        Month = 6,
                        Amount = 0,
                        Refund = 0
                    },
                    new StatisticMonth(){
                        Month = 7,
                        Amount = 0,
                        Refund = 0
                    },
                    new StatisticMonth(){
                        Month = 8,
                        Amount = 0,
                        Refund = 0
                    },
                    new StatisticMonth(){
                        Month = 9,
                        Amount = 0,
                        Refund = 0
                    },
                    new StatisticMonth(){
                        Month = 10,
                        Amount = 0,
                        Refund = 0
                    },
                    new StatisticMonth(){
                        Month = 11,
                        Amount = 0,
                        Refund = 0
                    },
                    new StatisticMonth(){
                        Month = 12,
                        Amount = 0,
                        Refund = 0
                    },
                };
                foreach (var transaction in transactions)
                {
                    foreach (var statisticMonth in returnValue)
                    {
                        if (transaction.CreateAt.HasValue && statisticMonth.Month == transaction.CreateAt.Value.Month)
                        {
                            if (transaction.PaymentRefund != null && transaction.PaymentRefund.Type == PaymentRefundEnum.Refund)
                            {
                                statisticMonth.Refund += transaction.Total;
                            }
                            else
                            {
                                statisticMonth.Amount += transaction.Total;
                            }
                        }
                    }
                }
                return SuccessResp.Ok(returnValue);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> BuyMembership(BuyMembershipRqDTO data)
        {
            try
            {
                Membership? membership = await _transactionRepo.GetMembership(data.MembershipId);
                if (membership == null)
                {
                    return ErrorResp.NotFound("Cant find membership");
                }
                string return_url = $"http://localhost:5101/transaction/Payment-Membership-Success?MembershipId={data.MembershipId}&UserId={data.UserId}";
                string cancel_url = "http://localhost:5173/";
                var payment = CreatePayment(membership.Price, return_url, cancel_url);
                var approvalUrl = payment.links.FirstOrDefault(lnk => lnk.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;
                return SuccessResp.Ok(approvalUrl);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> PaymentMembershipSuccess(Guid MembershipId, Guid UserId)
        {
            try
            {
                Membership? membership = await _transactionRepo.GetMembership(MembershipId);
                if (membership == null)
                {
                    return ErrorResp.NotFound("Cant find membership");
                }
                MembershipUser membershipUser = new()
                {
                    Status = true,
                    MembershipId = MembershipId,
                    UserId = UserId
                };
                var isCreatedMembershipUser = await _userRepo.CreateMembershipUser(membershipUser);
                if (!isCreatedMembershipUser)
                {
                    return ErrorResp.BadRequest("Cant create membership user");
                }

                MyTransaction transaction = new()
                {
                    TransactionType = TypeTransactionEnum.Membership,
                    Total = membership.Price,
                    UserId = UserId
                };
                var isCreatedTrasaction = await _transactionRepo.CreateTransaction(transaction);
                if (!isCreatedTrasaction)
                {
                    return ErrorResp.BadRequest("Cant create transaction");
                }
                return SuccessResp.Redirect("http://localhost:5173/");
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> TotalIncome()
        {
            try
            {
                float total = await _transactionRepo.TotalIncome();
                return SuccessResp.Ok(total);
            }
            catch (System.Exception ex)
            {
                return ErrorResp.BadRequest(ex.Message);
            }
        }
    }
}