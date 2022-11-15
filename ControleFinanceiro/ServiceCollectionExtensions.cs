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
            serviceCollection.AddTransient<IControleFinanceiroDatabase, ControleFinanceiroDatabase>();
            serviceCollection.AddDbContext<ControleFinanceiroDbContext>(options => options.UseInMemoryDatabase("controleFinanceiroDb"));
            return serviceCollection;
        }

        public static IServiceCollection AddSendGrid(this IServiceCollection serviceCollection)
        {
            var apiKey = "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";
            serviceCollection.AddSingleton(new SendGridClient(apiKey));
            serviceCollection.AddTransient<INotificationService, SendGridNotificationService>();
            return serviceCollection;
        }
    }
}
