namespace Hotel.Shared.Payments;

public interface IPaymentFactory
{
    IPayment? CreatePaymentCheckoutSession(string payMethod);
}