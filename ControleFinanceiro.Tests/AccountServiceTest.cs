using ControleFinanceiro.Accounts;
using ControleFinanceiro.Database;
using ControleFinanceiro.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Tests
{
    public class AccountServiceTests
    {
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
        public async void Release_ShouldThrowExeption_GivenInexistentEmail()
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
                await accountService.Release(financialRelease);
            });
        }

        [Theory( DisplayName = "Release should calculate the Account balance given a Financial Release.")]
        [InlineData(7000, FinancialReleaseType.Income, 500, 7500)]
        [InlineData(5000, FinancialReleaseType.Cost, 300, 4700)]
        [InlineData(0, FinancialReleaseType.Cost, 300, -300)]
        public async void Release_ShouldCalculateBalance_Given(decimal currentBalance, FinancialReleaseType releaseType, decimal value, decimal expectedBalance)
        {
            //Given
            var currentAccount = new Account { Email = "account1", Balance = currentBalance };
            var controleFinanceiroDatabase = GetDatabase();
            controleFinanceiroDatabase.Add(currentAccount);
            controleFinanceiroDatabase.Commit();
            var financialRelease = new FinancialReleaseInput
            {
                Email = currentAccount.Email,
                Type = releaseType,
                Value = value,
                Description = Guid.NewGuid().ToString(),
            };

            //When
            var accountService = new AccountService(controleFinanceiroDatabase);
            var balance = await accountService.Release(financialRelease);

            //Then
            Assert.Equal(expectedBalance, balance);
            var account = controleFinanceiroDatabase.Query<Account>().Include(e => e.FinancialReleases).Where(e=>e.Email == currentAccount.Email).First();
            Assert.Equal(1, account.FinancialReleases.Count);
        }
    }
}