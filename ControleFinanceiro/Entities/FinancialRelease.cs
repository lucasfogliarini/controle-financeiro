using ControleFinanceiro.Entities;

namespace ControleFinanceiro
{
    public class FinancialRelease : IEntity
    {
        public int Id { get; set; }
        public FinancialReleaseType Type { get; set; }
        public DateTime ReleaseAt { get; internal set; }
        public decimal Value { get; set; }
        public decimal CurrentBalance { get; internal set; }
        public string Description { get; set; }
    }

    public enum FinancialReleaseType
    {
        Cost = 0,
        Income = 1,
    }
}