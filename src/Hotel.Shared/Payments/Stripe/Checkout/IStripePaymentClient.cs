namespace Hotel.Shared.Payments.Stripe.Checkout;

public interface IStripePaymentClient
{
    Task<CustomerResource> CreateCustomer(CreateCustomerResource resource);
    Task<ChargeResource> CreateCharge(CreateChargeResource resource);
}
