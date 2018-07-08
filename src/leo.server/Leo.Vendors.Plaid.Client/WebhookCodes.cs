using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract]
    public enum WebhookCodes
    {
        WEBHOOK_UPDATE_ACKNOWLEDGED,
        INITIAL_UPDATE,
        HISTORICAL_UPDATE,
        DEFAULT_UPDATE,
        TRANSACTIONS_REMOVED,
        PRODUCT_READY,
        ERROR
    }
}