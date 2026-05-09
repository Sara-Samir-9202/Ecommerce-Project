using e_commerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace e_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly WalletService _walletService;

        public WalletController(WalletService walletService)
        {
            _walletService = walletService;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance()
        {
            var balance = await _walletService.GetBalanceAsync(GetUserId());
            return Ok(new { Balance = balance });
        }

        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _walletService.GetTransactionsAsync(GetUserId());
            return Ok(transactions);
        }
    }
}