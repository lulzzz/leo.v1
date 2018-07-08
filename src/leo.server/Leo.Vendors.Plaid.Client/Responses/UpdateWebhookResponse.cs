using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract]
    public class UpdateWebhookResponse
    {
        public UpdateWebhookResponse(Item item, string requestId)
        {
            Item = item;
            RequestId = requestId;
        }

        [DataMember(Name = "item")]
        public Item Item { get; private set; }

        [DataMember(Name = "request_id")]
        public string RequestId { get; private set; }
    }
}