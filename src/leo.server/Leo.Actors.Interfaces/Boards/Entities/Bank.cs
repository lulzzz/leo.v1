using System;

namespace Leo.Actors.Interfaces.Boards
{
    public class Bank
    {
        public Bank(string id, string name, string accessToken, DateTime createDate, DateTime? lastPollDate = null)
        {
            Id = id;
            Name = name;
            AccessToken = accessToken;
            CreateDate = createDate;
            LastPollDate = lastPollDate ?? DateTime.MinValue;
        }

        public string AccessToken { get; }

        public DateTime CreateDate { get; }

        public DateTime LastPollDate { get; set; }

        public Webhook Webhook { get; private set; }

        public string Id { get; }

        public string Name { get; }

        public void AddWebhook(string url)
        {
            Webhook = new Webhook(url);
        }
    }
}