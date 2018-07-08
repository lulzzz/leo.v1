using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract(Name = "balances")]
    public class Balances
    {
        [DataMember(Name = "available")]
        public decimal? Available { get; set; }

        [DataMember(Name = "current")]
        public decimal? Current { get; set; }

        [DataMember(Name = "limit")]
        public decimal? Limit { get; set; }
    }
}