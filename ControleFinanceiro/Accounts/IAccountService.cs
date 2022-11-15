namespace ControleFinanceiro.Accounts
{
    public interface IAccountService
    {
        Task<decimal> Release(FinancialReleaseInput financialRelease);
        IEnumerable<BalanceByDate> GetNegativeBalancesByDate(string email);
        IEnumerable<FinancialReleaseOutput> GetFinancialReleases(string email);
    }
}
