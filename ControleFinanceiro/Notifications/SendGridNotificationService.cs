using SendGrid.Helpers.Mail;
using SendGrid;
using ControleFinanceiro.Database;
using ControleFinanceiro.Entities;

namespace ControleFinanceiro.Notifications
{
    public class SendGridNotificationService : INotificationService
    {
        private readonly IControleFinanceiroDatabase _controleFinanceiroDatabase;
        readonly SendGridClient _sendGridClient;
        public SendGridNotificationService(SendGridClient sendGridClient, IControleFinanceiroDatabase controleFinanceiroDatabase)
        {
            _controleFinanceiroDatabase = controleFinanceiroDatabase;
            _sendGridClient = sendGridClient;
        }
        public async Task CheckBalancesAndNotifyAsync()
        {
            var accounts = _controleFinanceiroDatabase.Query<Account>().Where(e => e.Balance < 0);
            foreach (var account in accounts)
            {
                if (account.Balance < 0)
                {
                    var from = new EmailAddress("no-reply@controlefinanceiro.com", "Controle Financeiro");
                    var subject = $"Sua conta está com o saldo negativo! R${account.Balance}";
                    var to = new EmailAddress(account.Email, account.Name);
                    var htmlContent = subject;
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
                    var response = await _sendGridClient.SendEmailAsync(msg);
                }
            }
        }
    }
}
