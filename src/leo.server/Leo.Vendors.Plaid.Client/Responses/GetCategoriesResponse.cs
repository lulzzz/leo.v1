using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract]
    public class GetCategoriesResponse
    {
        [DataMember(Name = "categories")]
        public IEnumerable<Category> Categories { get; set; }
    }
}