using e_commerceAPI.Data;
using e_commerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerceAPI.Services
{
    public class WalletService
    {
        private readonly AppDbContext _context;

        public WalletService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetBalanceAsync(string userId)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            return wallet?.Balance ?? 0;
        }

        public async Task<bool> DepositAsync(string userId, decimal amount, string? transactionId = null)
        {
            if (amount <= 0) return false;

            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet == null)
            {
                wallet = new Wallet { UserId = userId, Balance = 0 };
                _context.Wallets.Add(wallet);
            }

            wallet.Balance += amount;
            wallet.UpdatedAt = DateTime.UtcNow;

            var transaction = new WalletTransaction
            {
                UserId = userId,
                Amount = amount,
                Type = "Deposit",
                TransactionId = transactionId,
                Status = "Completed"
            };
            _context.WalletTransactions.Add(transaction);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeductAsync(string userId, decimal amount, int orderId)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet == null || wallet.Balance < amount) return false;

            wallet.Balance -= amount;
            wallet.UpdatedAt = DateTime.UtcNow;

            var transaction = new WalletTransaction
            {
                UserId = userId,
                Amount = -amount,
                Type = "Payment",
                OrderId = orderId,
                Status = "Completed"
            };
            _context.WalletTransactions.Add(transaction);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<WalletTransaction>> GetTransactionsAsync(string userId)
        {
            return await _context.WalletTransactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
    }
}