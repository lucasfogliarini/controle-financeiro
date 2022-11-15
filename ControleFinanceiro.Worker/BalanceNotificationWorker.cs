using ControleFinanceiro.Notifications;

namespace ControleFinanceiro.Worker
{
    public class BalanceNotificationWorker : BackgroundService
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<BalanceNotificationWorker> _logger;

        public BalanceNotificationWorker(ILogger<BalanceNotificationWorker> logger, INotificationService notificationService)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await RunAsync();
            using PeriodicTimer timer = new(TimeSpan.FromSeconds(5));
            while (!stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                await RunAsync();
            }
        }

        private async Task RunAsync()
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await _notificationService.CheckBalancesAndNotifyAsync();
        }
    }
}