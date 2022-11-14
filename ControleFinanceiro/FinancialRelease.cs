namespace ControleFinanceiro
{
    public class FinancialRelease
    {
        public int Id { get; set; }
        public FinancialReleaseType Type { get; set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime ReleaseAt { get; internal set; }
        public decimal Value { get; set; }
        public decimal CurrentBalance { get; internal set; }
        public string? Description { get; set; }
    }

    public enum FinancialReleaseType
    {
        Cost = 0,
        Income = 1,
    }
}