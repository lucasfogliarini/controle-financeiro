using SendGrid.Helpers.Mail;
using SendGrid;

namespace ControleFinanceiro.Notifications
{
    internal class SendGridNotificationService : INotificationService
    {
        readonly SendGridClient _sendGridClient;
        public SendGridNotificationService(SendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }
        public async Task<bool> Notify(BalanceNotification balanceNotification)
        {
            var from = new EmailAddress("no-reply@controlefinanceiro.com", "Controle Financeiro");
            var subject = $"Sua conta está com o salgo negativo! R${balanceNotification.Balance}";
            var to = new EmailAddress(balanceNotification.AccountEmail, balanceNotification.AccountName);
            var htmlContent = "and easy to do anywhere, even with C#";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
            var response = await _sendGridClient.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }
    }
}
