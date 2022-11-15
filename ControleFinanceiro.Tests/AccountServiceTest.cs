using ControleFinanceiro.Accounts;
using ControleFinanceiro.Database;
using ControleFinanceiro.Entities;
using ControleFinanceiro.Notifications;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json.Linq;

namespace ControleFinanceiro.Tests
{
    public class AccountServiceTests
    {
        Mock<INotificationService> _mockNotificationService = new Mock<INotificationService>();

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
        public async void ReleaseAsync_ShouldThrowExeption_GivenInexistentEmail()
        {
            //Given
            var controleFinanceiroDatabase = GetDatabase();

            var financialRelease = new FinancialReleaseInput
            {
                Email = Guid.NewGuid().ToString(),
            };

            //When Then
             await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                var accountService = new AccountService(controleFinanceiroDatabase);
                await accountService.ReleaseAsync(financialRelease);
            });
        }

        [Theory( DisplayName = "Release should calculate the Account balance given a Financial Release.")]
        [InlineData(7000,  500, 7500)]
        [InlineData(5000, -300, 4700)]
        [InlineData(0, -300, -300)]
        public async void ReleaseAsync_ShouldCalculateBalance_Given(decimal currentBalance, decimal value, decimal expectedBalance)
        {
            //Given
            var currentAccount = new Account { Email = "account1", Balance = currentBalance };
            var controleFinanceiroDatabase = GetDatabase();
            controleFinanceiroDatabase.Add(currentAccount);
            controleFinanceiroDatabase.Commit();
            var financialRelease = new FinancialReleaseInput
            {
                Email = currentAccount.Email,
                Value = value,
                Description = Guid.NewGuid().ToString(),
            };

            //When
            var accountService = new AccountService(controleFinanceiroDatabase);
            var balance = await accountService.ReleaseAsync(financialRelease);

            //Then
            Assert.Equal(expectedBalance, balance);
            var account = controleFinanceiroDatabase.Query<Account>().Include(e => e.FinancialReleases).Where(e=>e.Email == currentAccount.Email).First();
            Assert.Equal(1, account.FinancialReleases.Count);
        }

        [Fact]
        public void GetNegativeBalancesByDate_ShouldThrowExeption_GivenInexistentEmail()
        {
            //Given
            var controleFinanceiroDatabase = GetDatabase();
            string email = Guid.NewGuid().ToString();

            //When Then
            Assert.Throws<ArgumentException>(() =>
            {
                var accountService = new AccountService(controleFinanceiroDatabase);
                accountService.GetNegativeBalancesByDate(email);
            });
        }
        [Fact]
        public async void GetNegativeBalancesByDate_ShouldReturnBalancesByDate_GivenFinancialReleases()
        {
            //Given
            var financialReleases = Seeder.GetFinancialReleases();
            var account = new Account { Email = "account1", Balance = 0 };
            var controleFinanceiroDatabase = GetDatabase();
            var accountService = new AccountService(controleFinanceiroDatabase);
            controleFinanceiroDatabase.Add(account);
            controleFinanceiroDatabase.Commit();

            foreach (var financialRelease in financialReleases)
            {
                var financialReleaseInput = new FinancialReleaseInput
                {
                    Email = account.Email,
                    Value = financialRelease.Value,
                    Description = financialRelease.Description,
                    ReleaseAt = financialRelease.ReleaseAt,
                };
                await accountService.ReleaseAsync(financialReleaseInput);
            }
            //When
            var balancesByDate = accountService.GetNegativeBalancesByDate(account.Email);

            //Then
            Assert.Equal(4, balancesByDate.Count());
            Assert.Contains(balancesByDate, e => e.Balance == -900);
            Assert.Contains(balancesByDate, e => e.Balance == -1200);

        }

        [Fact]
        public async void GetFinancialReleases_ShouldRerturnReleases()
        {
            //Given
            var currentAccount = new Account { Email = "account1", Balance = 0 };
            var controleFinanceiroDatabase = GetDatabase();
            controleFinanceiroDatabase.Add(currentAccount);
            controleFinanceiroDatabase.Commit();
            var financialRelease = new FinancialReleaseInput
            {
                Email = currentAccount.Email,
                Value = -1,
                Description = Guid.NewGuid().ToString(),
            };
            var accountService = new AccountService(controleFinanceiroDatabase);
            await accountService.ReleaseAsync(financialRelease);

            //When
            var financialReleases = accountService.GetFinancialReleases(currentAccount.Email);

            //Then
            Assert.Equal(1, financialReleases?.Count());
            Assert.Contains(financialReleases, e=>e.Value == -1);
        }
    }
}