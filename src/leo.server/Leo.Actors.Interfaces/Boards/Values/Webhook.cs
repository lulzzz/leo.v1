namespace Leo.Actors.Interfaces.Boards
{
    public enum WebhookStates
    {
        Unacknowledged = 0,
        Acknowledged = 1,
        Failed = 2
    }

    public class Webhook
    {
        public Webhook(string url, WebhookStates state = WebhookStates.Unacknowledged)
        {
            Url = url;
            State = state;
        }

        public WebhookStates State { get; private set; }

        public string Url { get; }
    }
}