namespace ControleFinanceiro.Accounts
{
    public class FinancialReleaseOutput
    {
        public string Email { get; set; }
        public FinancialReleaseType Type { get; set; }
        public decimal Value { get; set; }
        public DateTime ReleaseAt { get; set; }
        public string Description { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
