using Flurl.Http;
using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract]
    [RequestRoute("item/public_token/exchange")]
    public class ExchangeTokenRequest
    {
        public ExchangeTokenRequest(string clientId, string secret, string publicToken)
        {
            ClientId = clientId;
            Secret = secret;
            PublicToken = publicToken;
        }

        [DataMember(Name = "client_id")]
        public string ClientId { get; private set; }

        [DataMember(Name = "public_token")]
        public string PublicToken { get; private set; }

        [DataMember(Name = "secret")]
        public string Secret { get; private set; }
    }
}