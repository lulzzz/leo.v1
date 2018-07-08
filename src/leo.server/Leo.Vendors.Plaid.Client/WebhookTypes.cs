using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract]
    public enum WebhookTypes
    {
        [EnumMember(Value = "ITEM")]
        Item,

        [EnumMember(Value = "TRANSACTIONS")]
        Transactions
    }
}