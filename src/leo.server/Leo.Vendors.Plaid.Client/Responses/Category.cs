using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract]
    public class Category
    {
        [DataMember(Name = "category_id")]
        public string CategoryId { get; set; }

        [DataMember(Name = "group")]
        public string Group { get; set; }

        [DataMember(Name = "hierarchy")]
        public string[] Hierarchy { get; set; }
    }
}