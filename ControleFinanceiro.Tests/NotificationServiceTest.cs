using ControleFinanceiro.Accounts;
using ControleFinanceiro.Database;
using ControleFinanceiro.Entities;
using ControleFinanceiro.Notifications;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ControleFinanceiro.Tests
{
    public class NotificationServiceTest
    {
        ISendGridClient _sendGridClientMock = Substitute.For<ISendGridClient>();

        private IControleFinanceiroDatabase GetDatabase()
        {
            var options = new DbContextOptionsBuilder<ControleFinanceiroDbContext>()
               .UseInMemoryDatabase(databaseName: "controleFinanceiroDb")
               .Options;
            var controleFinanceiroDbContext = new ControleFinanceiroDbContext(options);
            controleFinanceiroDbContext.Database.EnsureDeleted();
            return new ControleFinanceiroDatabase(controleFinanceiroDbContext);
        }

        [Fact]
        public async void CheckBalancesAndNotifyAsync_ShouldNotifyAccounts_GivenBalanceLessThan0()
        {
            //Given
            var controleFinanceiroDatabase = GetDatabase();
            controleFinanceiroDatabase.Add(new Account { Email = "account1", Balance = -1 });
            controleFinanceiroDatabase.Add(new Account { Email = "account2", Balance = -2 });
            controleFinanceiroDatabase.Commit();

            //When
            var notificationService = new SendGridNotificationService(_sendGridClientMock, controleFinanceiroDatabase);
            await notificationService.CheckBalancesAndNotifyAsync();

            //Then
            await _sendGridClientMock.Received(2).SendEmailAsync(Arg.Any<SendGridMessage>());
        }
    }
}