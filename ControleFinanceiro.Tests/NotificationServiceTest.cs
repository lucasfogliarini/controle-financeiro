using ControleFinanceiro.Accounts;
using ControleFinanceiro.Database;
using ControleFinanceiro.Entities;
using ControleFinanceiro.Notifications;
using Microsoft.EntityFrameworkCore;
using Moq;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ControleFinanceiro.Tests
{
    public class NotificationServiceTest
    {
        Mock<ISendGridClient> _mockSendGridClient = new();

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
            var currentAccount = new Account { Email = "account1", Balance = 0 };
            var controleFinanceiroDatabase = GetDatabase();
            controleFinanceiroDatabase.Add(currentAccount);
            controleFinanceiroDatabase.Commit();

            //When
            var notificationService = new SendGridNotificationService(_mockSendGridClient.Object, controleFinanceiroDatabase);
            await notificationService.CheckBalancesAndNotifyAsync();

            //Then
            new Mock<INotificationService>().Verify(e=>e.CheckBalancesAndNotifyAsync(), Times.Once);
            //_mockSendGridClient.Verify(e => e.SendEmailAsync(msg: It.IsAny<SendGridMessage>()), Times.Once);
        }
    }
}