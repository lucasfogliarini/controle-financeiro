namespace ControleFinanceiro.Accounts
{
    public interface IAccountService
    {
        Task<decimal> Release(FinancialReleaseInput financialRelease);
    }
}
