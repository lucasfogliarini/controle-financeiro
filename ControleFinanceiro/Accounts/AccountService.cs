using ControleFinanceiro.Database;
using ControleFinanceiro.Entities;
using ControleFinanceiro.Notifications;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Accounts
{
    public class AccountService : IAccountService
    {
        readonly IControleFinanceiroDatabase _controleFinanceiroDatabase;
        private readonly INotificationService _notificationService;

        public AccountService(IControleFinanceiroDatabase controleFinanceiroDatabase, INotificationService notificationService)
        {
            _controleFinanceiroDatabase = controleFinanceiroDatabase;
            _notificationService = notificationService;
        }

        public async Task<decimal> Release(FinancialReleaseInput financialReleaseInput)
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

            await Notify(account);

            return account.Balance;
        }

        public IEnumerable<BalanceByDate> GetBalancesByDate(string email)
        {
            var account = GetAccount(email);
            var balancesByDate = account.FinancialReleases.GroupBy(e => e.ReleaseAt.Date).Select(e=> new BalanceByDate { Date = e.Key, Balance = e.Sum(e=>e.Value) });
            return balancesByDate;
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

        private async Task Notify(Account account)
        {
            if (account.Balance < 0)
            {
                var balanceNotification = new BalanceNotification
                {
                    AccountName = account.Name,
                    AccountEmail = account.Email,
                    Balance = account.Balance,
                };

                await _notificationService.Notify(balanceNotification);
            }
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
