namespace Leo.Actors.Boards
{
    public class TransactionWebhookConfig
    {
        public const string SettingsPrefix = "transactions.webhook";

        public string Url { get; set; }
    }
}