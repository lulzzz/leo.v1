using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract(Name = "account")]
    public class Account
    {
        [DataMember(Name = "account_id")]
        public string AccountId { get; set; }

        [DataMember(Name = "balances")]
        public Balances Balances { get; set; }

        [DataMember(Name = "mask")]
        public string Mask { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "official_name")]
        public string OfficialName { get; set; }

        [DataMember(Name = "subtype")]
        public string SubType { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "numbers")]
        public Numbers Numbers { get; set; }
    }
}