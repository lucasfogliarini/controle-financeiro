namespace ControleFinanceiro.Accounts
{
    public class FinancialReleaseInput
    {
        public string Email { get; set; }
        public FinancialReleaseType Type { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
    }
}
