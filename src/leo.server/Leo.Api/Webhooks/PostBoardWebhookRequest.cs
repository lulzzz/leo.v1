using Leo.Core.Api.Interfaces;
using Leo.Vendors.Plaid.Client;
using System.Runtime.Serialization;

namespace Leo.Api.Webhooks
{
    [DataContract]
    public class PostBoardWebhookRequest : IRequest
    {
        public PostBoardWebhookRequest(string boardId, WebhookTypes webhookType, WebhookCodes webhookCode, string itemId, int newTransactions, ItemError error = null)
        {
            BoardId = boardId;
            WebhookType = webhookType;
            WebhookCode = webhookCode;
            ItemId = itemId;
            NewTransactions = newTransactions;
            Error = error;
        }

        [DataMember(Name = "boardid")]
        public string BoardId { get; private set; }

        [DataMember(Name = "error")]
        public ItemError Error { get; private set; }

        [DataMember(Name = "item_id")]
        public string ItemId { get; private set; }

        [DataMember(Name = "new_transactions")]
        public int NewTransactions { get; private set; }

        [DataMember(Name = "webhook_code")]
        public WebhookCodes WebhookCode { get; private set; }

        [DataMember(Name = "webhook_type")]
        public WebhookTypes WebhookType { get; private set; }
    }
}