using ControleFinanceiro.Database;
using ControleFinanceiro.Entities;
using ControleFinanceiro.Notifications;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Accounts
{
    public class AccountService : IAccountService
    {
        readonly IControleFinanceiroDatabase _controleFinanceiroDatabase;

        public AccountService(IControleFinanceiroDatabase controleFinanceiroDatabase)
        {
            _controleFinanceiroDatabase = controleFinanceiroDatabase;
        }

        public async Task<decimal> ReleaseAsync(FinancialReleaseInput financialReleaseInput)
        {
            var account = GetAccount(financialReleaseInput.Email);
            account.Balance += financialReleaseInput.Value;
            var financialRelease = new FinancialRelease
            {
                AccountId = account.Id,
                Value = financialReleaseInput.Value,
                Description = financialReleaseInput.Description,
                ReleaseAt = financialReleaseInput.ReleaseAt,
                CurrentBalance = account.Balance,
            };

            _controleFinanceiroDatabase.Update(account);
            _controleFinanceiroDatabase.Add(financialRelease);
            await _controleFinanceiroDatabase.CommitAsync();

            return account.Balance;
        }

        public IEnumerable<BalanceByDate> GetNegativeBalancesByDate(string email)
        {
            var account = GetAccount(email);
            var balancesByDate = account.FinancialReleases.GroupBy(e => e.ReleaseAt.Date).Select(e=> new BalanceByDate { Date = e.Key, Balance = e.Sum(e=>e.Value) });
            return balancesByDate.Where(e=>e.Balance < 0);
        }

        private Account GetAccount(string email)
        {
            var account = _controleFinanceiroDatabase.Query<Account>()
                                                     .Include(e=>e.FinancialReleases)
                                                     .FirstOrDefault(e => e.Email == email);
            if (account == null)
            {
                throw new ArgumentException($"Conta não encontrada. Email: {email}");
            }
            return account;
        }

        public IEnumerable<FinancialReleaseOutput> GetFinancialReleases(string email)
        {
            var account = GetAccount(email);
            foreach (var financialRelease in account.FinancialReleases)
            {
                yield return new FinancialReleaseOutput
                {
                    Email = financialRelease.Account.Email,
                    Type = financialRelease.Type,
                    ReleaseAt = financialRelease.ReleaseAt,
                    Value = financialRelease.Value,
                    Description  = financialRelease.Description,
                    CurrentBalance = financialRelease.CurrentBalance
                };
            }
        }
    }
}
