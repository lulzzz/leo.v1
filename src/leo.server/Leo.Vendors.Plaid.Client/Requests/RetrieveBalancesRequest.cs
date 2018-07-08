using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract]
    [RequestRoute("/accounts/balance/get")]
    public class RetrieveBalancesRequest
    {
        public RetrieveBalancesRequest(string clientId, string secret, string accessToken, IEnumerable<string> accountIds = null)
        {
            Options = new RetrieveBalancesOptions(accountIds);
            AccessToken = accessToken;
            ClientId = clientId;
            Secret = secret;
        }

        [DataMember(Name = "access_token")]
        public string AccessToken { get; private set; }

        [DataMember(Name = "client_id")]
        public string ClientId { get; private set; }

        [DataMember(Name = "options", EmitDefaultValue = false)]
        public RetrieveBalancesOptions Options { get; private set; }

        [DataMember(Name = "secret")]
        public string Secret { get; private set; }
    }

    [DataContract]
    public class RetrieveBalancesOptions
    {
        public RetrieveBalancesOptions(IEnumerable<string> accountIds = null)
        {
            AccountIds = accountIds;
        }

        [DataMember(Name = "account_ids", EmitDefaultValue = false)]
        public IEnumerable<string> AccountIds { get; private set; }
    }
}