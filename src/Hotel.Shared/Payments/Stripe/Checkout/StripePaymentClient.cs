using Stripe;

namespace Hotel.Shared.Payments.Stripe.Checkout;

/*
    Custom build in: 
        - có chức năng add payment vào trong hệ thống
        - mỗi lần thanh toán sẽ gọi API của stripe và cập nhật
*/

internal class StripePaymentClient : IStripePaymentClient
{
    private readonly TokenService _tokenService;
    private readonly CustomerService _customerService;
    private readonly ChargeService _chargeService;
    public StripePaymentClient(
        TokenService tokenService,
        CustomerService customerService,
        ChargeService chargeService)
    {
        _tokenService = tokenService;
        _customerService = customerService;
        _chargeService = chargeService;
    }

    // document at: https://stripe.com/docs/payments/accept-a-payment?platform=web&ui=elements
    public async Task<ChargeResource> CreateCharge(CreateChargeResource resource)
    {
        // customer id = user id
        var chargeOptions = new ChargeCreateOptions
        {
            Currency = resource.Currency,
            Amount = resource.Amount,
            ReceiptEmail = resource.ReceiptEmail,
            // call customer id on customer service to check it
            Customer = resource.CustomerId,
            Description = resource.Description
            // metadata to bind product
        };

        var charge = await _chargeService.CreateAsync(chargeOptions, null);

        return new ChargeResource(
            charge.Id,
            charge.Currency,
            charge.Amount,
            charge.CustomerId,
            charge.ReceiptEmail,
            charge.Description);
    }

    public async Task<CustomerResource> CreateCustomer(CreateCustomerResource resource)
    {
        var tokenOptions = new TokenCreateOptions
        {
            Card = new TokenCardOptions
            {
                Name = resource.Card.Name,
                Number = resource.Card.Number,
                ExpYear = resource.Card.ExpiryYear,
                ExpMonth = resource.Card.ExpiryMonth,
                Cvc = resource.Card.Cvc
            }
        };
        var token = await _tokenService.CreateAsync(tokenOptions, null);

        var customerOptions = new CustomerCreateOptions
        {
            Email = resource.Email,
            Name = resource.Name,
            Source = token.Id
        };
        var customer = await _customerService.CreateAsync(customerOptions, null);

        return new CustomerResource(customer.Id, customer.Email, customer.Name);
    }
}
