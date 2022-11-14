using ControleFinanceiro.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFinanceiro.EntityFrameworkCore
{
    internal sealed class AccountCofiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Name);
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.Balance).IsRequired();
        }
    }
}
