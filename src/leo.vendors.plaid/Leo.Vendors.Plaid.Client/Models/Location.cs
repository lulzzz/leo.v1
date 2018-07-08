using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract(Name = "location")]
    public class Location
    {
        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "coordinates")]
        public Coordinates Coordinates { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }

        [DataMember(Name = "store_number")]
        public string StoreNumber { get; set; }

        [DataMember(Name = "zip")]
        public string Zip { get; set; }
    }
}