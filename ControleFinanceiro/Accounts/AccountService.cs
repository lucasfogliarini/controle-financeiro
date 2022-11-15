using ControleFinanceiro.Database;
using ControleFinanceiro.Entities;
using System.Net.Mail;
using System.Net;
using SendGrid;
using SendGrid.Helpers.Mail;
using ControleFinanceiro.Notifications;
using System.Security.Principal;

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
            var account = _controleFinanceiroDatabase.Query<Account>().FirstOrDefault(e=>e.Email == financialReleaseInput.Email);
            if (account == null) 
            {
                throw new ArgumentException($"Conta não encontrada. Email: {financialReleaseInput.Email}");
            }

            if (financialReleaseInput.Type == FinancialReleaseType.Income)
            {
                account.Balance += financialReleaseInput.Value;
            }
            else
            {
                account.Balance -= financialReleaseInput.Value;
            }
            _controleFinanceiroDatabase.Update(account);

            var financialRelease = new FinancialRelease
            {
                AccountId = account.Id,
                Type = financialReleaseInput.Type,
                Value = financialReleaseInput.Value,
                Description = financialReleaseInput.Description,
                ReleaseAt = DateTime.Now,
            };
            _controleFinanceiroDatabase.Add(financialRelease);
            await _controleFinanceiroDatabase.CommitAsync();
            await Notify(account);

            return account.Balance;
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
    }
}
