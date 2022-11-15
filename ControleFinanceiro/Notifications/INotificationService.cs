namespace ControleFinanceiro.Notifications
{
    public interface INotificationService
    {
        Task CheckBalancesAndNotifyAsync();
    }
}
