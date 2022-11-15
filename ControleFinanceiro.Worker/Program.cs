using ControleFinanceiro;
using ControleFinanceiro.Accounts;
using ControleFinanceiro.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostBuilder,services) =>
    {
        var sendGridApiKey = hostBuilder.Configuration["SendGridApiKey"];
        services.AddServices()
                .AddSendGrid(sendGridApiKey)
                .AddHostedService<BalanceNotificationWorker>();
    })
.Build();

await Seed();

host.Run();

async Task Seed()
{
    await host.Services.GetService<SeedService>().Seed();
    var accountService = host.Services.GetService<IAccountService>();

    var financialRelease = new FinancialReleaseInput
    {
        ReleaseAt = DateTime.Now,
        Email = "lucasfogliarini@gmail.com",
        Value = -10000,
        Description = "Uma despesa pesada"
    };
    await accountService.ReleaseAsync(financialRelease);
}
