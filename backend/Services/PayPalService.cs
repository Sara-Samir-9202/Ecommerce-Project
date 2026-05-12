using System.Text;
using System.Text.Json;
using e_commerceAPI.Data;
using e_commerceAPI.Models;

namespace e_commerceAPI.Services
{
    public class PayPalService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;
        private readonly bool _isEnabled;

        public PayPalService(IConfiguration config, HttpClient httpClient, AppDbContext context)
        {
            _config = config;
            _httpClient = httpClient;
            _context = context;
            _isEnabled = _config.GetValue<bool>("PayPal:Enabled");
        }

        private async Task<string> GetAccessToken()
        {
            if (!_isEnabled) return string.Empty;

            var clientId = _config["PayPal:ClientId"];
            var secret = _config["PayPal:Secret"];

            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{secret}"));

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", auth);

            var content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _httpClient.PostAsync("https://api-m.sandbox.paypal.com/v1/oauth2/token", content);
            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("access_token").GetString() ?? string.Empty;
        }

        public async Task<string?> CreateOrder(decimal amount, int orderId, string returnUrl, string cancelUrl)
        {
            if (!_isEnabled) return null;

            var accessToken = await GetAccessToken();
            if (string.IsNullOrEmpty(accessToken)) return null;

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var orderRequest = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
                    new
                    {
                        reference_id = orderId.ToString(),
                        amount = new
                        {
                            currency_code = "USD",
                            value = amount.ToString("0.00")
                        }
                    }
                },
                application_context = new
                {
                    return_url = returnUrl,
                    cancel_url = cancelUrl,
                    brand_name = "E-Commerce Store",
                    user_action = "PAY_NOW"
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(orderRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://api-m.sandbox.paypal.com/v2/checkout/orders", content);
            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var links = doc.RootElement.GetProperty("links");

            foreach (var link in links.EnumerateArray())
            {
                if (link.GetProperty("rel").GetString() == "approve")
                {
                    return link.GetProperty("href").GetString();
                }
            }

            return null;
        }
    }
}