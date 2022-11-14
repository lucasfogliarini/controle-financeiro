using ControleFinanceiro.Database;
using ControleFinanceiro.Entities;

namespace ControleFinanceiro.Accounts
{
    public class AccountService : IAccountService
    {
        readonly IControleFinanceiroDatabase _controleFinanceiroDatabase;

        public AccountService(IControleFinanceiroDatabase controleFinanceiroDatabase)
        {
            _controleFinanceiroDatabase = controleFinanceiroDatabase;
        }

        public async Task<decimal> Release(FinancialReleaseInput financialReleaseInput)
        {
            var account = _controleFinanceiroDatabase.Query<Account>().FirstOrDefault(e=>e.Email == financialReleaseInput.Email);
            if (account == null) 
            {
                throw new Exception($"Conta não encontrada. Email: {financialReleaseInput.Email}");
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

            return account.Balance;
        }
    }
}
