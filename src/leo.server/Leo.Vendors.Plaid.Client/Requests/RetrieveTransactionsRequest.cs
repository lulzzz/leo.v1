using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract]
    public class RetrieveTransactionsOptions
    {
        public const ushort DefaultCount = 100;
        public const uint DefaultOffset = 0;

        public RetrieveTransactionsOptions(IEnumerable<string> accountIds = null, ushort count = DefaultCount, uint offset = DefaultOffset)
        {
            AccountIds = accountIds;
            Count = count;
            Offset = offset;
        }
        
        [DataMember(Name = "account_ids", EmitDefaultValue = false)]
        public IEnumerable<string> AccountIds { get; private set; }

        [DataMember(Name = "count")]
        public ushort Count { get; private set; }

        [DataMember(Name = "offset")]
        public uint Offset { get; private set; }
    }

    [DataContract]
    [RequestRoute("transactions/get")]
    public class RetrieveTransactionsRequest
    {
        public RetrieveTransactionsRequest(string clientId, string secret, string accessToken, DateTime start, DateTime end, IEnumerable<string> accountIds = null, ushort count = RetrieveTransactionsOptions.DefaultCount, uint offset = RetrieveTransactionsOptions.DefaultOffset)
        {
            Options = new RetrieveTransactionsOptions(accountIds, count, offset);
            AccessToken = accessToken;
            ClientId = clientId;
            Secret = secret;
            EndDate = end.ToString("yyyy-MM-dd");
            StartDate = start.ToString("yyyy-MM-dd");
        }

        [DataMember(Name = "access_token")]
        public string AccessToken { get; private set; }

        [DataMember(Name = "client_id")]
        public string ClientId { get; private set; }

        [DataMember(Name = "end_date")]
        public string EndDate { get; private set; }

        [DataMember(Name = "options", EmitDefaultValue = false)]
        public RetrieveTransactionsOptions Options { get; private set; }

        [DataMember(Name = "secret")]
        public string Secret { get; private set; }

        [DataMember(Name = "start_date")]
        public string StartDate { get; private set; }
    }
}