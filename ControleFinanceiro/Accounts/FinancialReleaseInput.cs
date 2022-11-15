namespace ControleFinanceiro.Accounts
{
    public class FinancialReleaseInput
    {
        public string Email { get; set; }
        public decimal Value { get; set; }
        public DateTime ReleaseAt { get; set; }
        public string Description { get; set; }
    }
}
