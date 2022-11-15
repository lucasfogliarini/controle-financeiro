namespace ControleFinanceiro.Accounts
{
    public interface IAccountService
    {
        Task<decimal> Release(FinancialReleaseInput financialRelease);
        IEnumerable<BalanceByDate> GetBalancesByDate(string email);
    }
}
