using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFinanceiro.EntityFrameworkCore
{
    internal sealed class FinancialReleaseCofiguration : IEntityTypeConfiguration<FinancialRelease>
    {
        public void Configure(EntityTypeBuilder<FinancialRelease> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Type).IsRequired();
            builder.Property(e => e.ReleaseAt).IsRequired();
            builder.Property(e => e.Value).IsRequired();
            builder.Property(e => e.Description).IsRequired();
        }
    }
}
