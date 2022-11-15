using ControleFinanceiro.Accounts;
using ControleFinanceiro.Database;
using ControleFinanceiro.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;

namespace ControleFinanceiro
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAccountService, AccountService>();
            serviceCollection.AddTransient<SeedService>();
            serviceCollection.AddTransient<IControleFinanceiroDatabase, ControleFinanceiroDatabase>();
            serviceCollection.AddDbContext<ControleFinanceiroDbContext>(options => options.UseInMemoryDatabase("controleFinanceiroDb"), ServiceLifetime.Singleton);
            return serviceCollection;
        }

        public static IServiceCollection AddSendGrid(this IServiceCollection serviceCollection, string apiKey)
        {
            serviceCollection.AddSingleton<ISendGridClient>(new SendGridClient(apiKey));
            serviceCollection.AddTransient<INotificationService, SendGridNotificationService>();
            return serviceCollection;
        }
    }
}
