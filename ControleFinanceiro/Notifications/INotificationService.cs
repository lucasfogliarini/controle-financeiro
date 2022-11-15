namespace ControleFinanceiro.Notifications
{
    public interface INotificationService
    {
        Task<bool> Notify(BalanceNotification balanceNotification);
    }
}
