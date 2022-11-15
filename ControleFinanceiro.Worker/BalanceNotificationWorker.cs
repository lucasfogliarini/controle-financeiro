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
            await _notificationService.CheckBalancesAndNotifyAsync();
            using PeriodicTimer timer = new(TimeSpan.FromSeconds(10));
            while (!stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _notificationService.CheckBalancesAndNotifyAsync();
            }
        }
    }
}