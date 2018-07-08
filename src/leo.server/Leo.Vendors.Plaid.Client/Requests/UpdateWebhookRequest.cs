using Flurl.Http;
using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract]
    [RequestRoute("/item/webhook/update")]
    public class UpdateWebhookRequest
    {
        public UpdateWebhookRequest(string clientId, string secret, string accessToken, string webhook)
        {
            ClientId = clientId;
            Secret = secret;
            AccessToken = accessToken;
            Webhook = webhook;
        }

        [DataMember(Name = "access_token")]
        public string AccessToken { get; private set; }

        [DataMember(Name = "client_id")]
        public string ClientId { get; private set; }

        [DataMember(Name = "secret")]
        public string Secret { get; private set; }

        [DataMember(Name = "webhook")]
        public string Webhook { get; private set; }
    }
}