using ControleFinanceiro.Accounts;
using ControleFinanceiro.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFinanceiro
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAccountService, AccountService>();
            serviceCollection.AddTransient<IControleFinanceiroDatabase, ControleFinanceiroDatabase>();
            serviceCollection.AddDbContext<ControleFinanceiroDbContext>(options => options.UseInMemoryDatabase("controleFinanceiroDb"));
            return serviceCollection;
        }
    }
}
