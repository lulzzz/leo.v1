using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    public class RetrieveTransactionsResponse
    {
        [DataMember(Name = "accounts")]
        public IEnumerable<Account> Accounts { get; set; }

        [DataMember(Name = "request_id")]
        public string RequestId { get; set; }

        [DataMember(Name = "transactions")]
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}