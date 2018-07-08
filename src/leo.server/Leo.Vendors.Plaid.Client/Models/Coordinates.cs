using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract(Name = "coordinates")]
    public class Coordinates
    {
        [DataMember(Name = "lat")]
        public decimal Latitude { get; set; }

        [DataMember(Name = "lon")]
        public decimal Longitude { get; set; }
    }
}