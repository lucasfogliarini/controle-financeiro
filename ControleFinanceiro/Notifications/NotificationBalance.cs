namespace ControleFinanceiro.Notifications
{
    public class BalanceNotification
    {
        public string AccountName { get; set; }
        public string AccountEmail { get; set; }
        public decimal Balance { get; set; }
    }
}
