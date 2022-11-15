using ControleFinanceiro.Accounts;
using ControleFinanceiro.Database;
using ControleFinanceiro.Entities;

namespace ControleFinanceiro
{
    public class SeedService
    {
        private readonly IAccountService _accountService;
        private readonly IControleFinanceiroDatabase _controleFinanceiroDatabase;

        public SeedService(IControleFinanceiroDatabase controleFinanceiroDatabase, IAccountService accountService)
        {
            _accountService = accountService;
            _controleFinanceiroDatabase = controleFinanceiroDatabase;
        }

        public async Task<string> Seed()
        {
            var account = new Account
            {
                Name = "Lucas Fogliarini",
                Email = "lucasfogliarini@gmail.com",
                Balance = 0
            };
            _controleFinanceiroDatabase.Add(account);
            _controleFinanceiroDatabase.Commit();

            var financialReleases = GetFinancialReleases();
            foreach (var financialRelease in financialReleases)
            {
                var financialReleaseInput = new FinancialReleaseInput
                {
                    Email = account.Email,
                    Value = financialRelease.Value,
                    Description = financialRelease.Description,
                    ReleaseAt = financialRelease.ReleaseAt,
                };
                await _accountService.ReleaseAsync(financialReleaseInput);
            }

            return account.Email;
        }

        public IEnumerable<FinancialRelease> GetFinancialReleases()
        {
            var financialReleases = new List<FinancialRelease>()
            {
                new FinancialRelease 
                { ReleaseAt = new DateTime(2022, 8, 29), Description = "Cartão de Crédito", Value = -825.82m },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 8, 29), Description = "Curso C#", Value = -200 },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 8, 31), Description = "Salário", Value = 7000 },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 9, 1), Description = "Mercado", Value = -3000 },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 9, 1), Description = "Farmácia", Value = -300 },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 9, 1), Description = "Combustível", Value = -800.25m },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 9, 15), Description = "Financiamento Carro", Value = -900 },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 9, 22), Description = "Financiamento Casa", Value = -1200 },
                new FinancialRelease
                { ReleaseAt = new DateTime(2022, 9, 25), Description = "Freelance Projeto XPTO", Value = 2500 },
            };

            return financialReleases;
        }
    }
}
