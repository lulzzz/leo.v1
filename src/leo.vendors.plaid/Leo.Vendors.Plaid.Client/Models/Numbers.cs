using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract(Name = "numbers")]
    public class Numbers
    {
        [DataMember(Name = "account")]
        public string Account { get; set; }

        [DataMember(Name = "account_id")]
        public string AccountId { get; set; }

        [DataMember(Name = "routing")]
        public string Routing { get; set; }

        [DataMember(Name = "wire_routing")]
        public string WireRouting { get; set; }
    }
}