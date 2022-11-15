using ControleFinanceiro.Entities;

namespace ControleFinanceiro
{
    public class FinancialRelease : IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public FinancialReleaseType Type { get { return Value > 0 ? FinancialReleaseType.Income : FinancialReleaseType.Cost; } }
        public DateTime ReleaseAt { get; internal set; }
        public decimal Value { get; set; }
        public decimal CurrentBalance { get; set; }
        public string Description { get; set; }
    }

    public enum FinancialReleaseType
    {
        Cost = 0,
        Income = 1,
    }
}