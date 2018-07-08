using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract]
    public class RetrieveAuthResponse
    {
        [DataMember(Name = "accounts")]
        public IEnumerable<Account> Accounts { get; set; }

        [DataMember(Name = "request_id")]
        public string RequestId { get; set; }
    }
}