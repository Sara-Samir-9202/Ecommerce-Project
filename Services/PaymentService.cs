using e_commerceAPI.DTO.Payment;
using e_commerceAPI.Models;
using Stripe;
using Stripe.Checkout;

namespace e_commerceAPI.Services
{
    public class PaymentService
    {
        private readonly IConfiguration _config;

        public PaymentService(IConfiguration config)
        {
            _config = config;
            StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
        }

        public async Task<PaymentResponseDTO> CreateStripePaymentIntent(int orderId, decimal amount, string successUrl, string cancelUrl)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"Order #{orderId}"
                            },
                            UnitAmount = (long)(amount * 100)
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return new PaymentResponseDTO
            {
                ClientSecret = session.Id,
                RedirectUrl = session.Url,
                RequiresAction = false
            };
        }

        public async Task<bool> VerifyPayment(string sessionId)
        {
            var service = new SessionService();
            var session = await service.GetAsync(sessionId);
            return session.PaymentStatus == "paid";
        }
    }
}