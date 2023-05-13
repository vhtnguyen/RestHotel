namespace Hotel.Shared.Payments.Stripe.Link;

public interface IStripeCheckoutClient
{
    Task<SessionResource> CreateSession(CreateSessionResource resource);
}
