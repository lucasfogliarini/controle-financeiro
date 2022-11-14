namespace ControleFinanceiro
{
    public class FinancialRelease
    {
        public int Id { get; set; }
        public FinancialReleaseType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ReleaseAt { get; set; }
        public decimal Value { get; set; }
        public string? Description { get; set; }
    }

    public enum FinancialReleaseType
    {
        Cost = 0,
        Income = 1,
    }
}