using Hotel.BusinessLogic.DTO.Payment;
using Hotel.Shared.Payments;

namespace Hotel.BusinessLogic.Services.IServices;

public interface IPaymentService
{
    Task<SessionResource> CreatePaymentLink(int invoiceId, CreatePaymentDto payment);
    Task PayFailed(string paymentIntentId);
    Task PaySucceed(string paymentIntentId);
}