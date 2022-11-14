using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ControleFinanceiro.Database
{
    public class ControleFinanceiroDbContext : DbContext
    {
        public ControleFinanceiroDbContext(DbContextOptions<ControleFinanceiroDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(thisAssembly);
        }
    }
}
