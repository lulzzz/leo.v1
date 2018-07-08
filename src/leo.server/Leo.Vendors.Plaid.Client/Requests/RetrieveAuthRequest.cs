using Flurl.Http;
using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract]
    [RequestRoute("auth/get")]
    public class RetrieveAuthRequest
    {
        public RetrieveAuthRequest(string clientId, string secret, string accessToken)
        {
            ClientId = clientId;
            Secret = secret;
            AccessToken = accessToken;
        }

        [DataMember(Name = "access_token")]
        public string AccessToken { get; private set; }

        [DataMember(Name = "client_id")]
        public string ClientId { get; private set; }

        [DataMember(Name = "secret")]
        public string Secret { get; private set; }
    }
}