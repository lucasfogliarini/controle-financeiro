using ControleFinanceiro;
using ControleFinanceiro.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddServices()
                .AddSendGrid()
                .AddHostedService<BalanceNotificationWorker>();
    })
    .Build();

host.Run();
