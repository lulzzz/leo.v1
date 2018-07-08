using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract]
    public class RetrieveBalancesResponse
    {
        [DataMember(Name = "accounts")]
        public IEnumerable<Account> Accounts { get; set; }

        [DataMember(Name = "item")]
        public Item Item { get; set; }

        [DataMember(Name = "request_id")]
        public string RequestId { get; set; }
    }
}