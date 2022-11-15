using ControleFinanceiro.Entities;

namespace ControleFinanceiro.Accounts
{
    public interface IAccountService
    {
        Task<decimal> ReleaseAsync(FinancialReleaseInput financialRelease);
        IEnumerable<BalanceByDate> GetNegativeBalancesByDate(string email);
        IEnumerable<FinancialReleaseOutput> GetFinancialReleases(string email);
    }
}
