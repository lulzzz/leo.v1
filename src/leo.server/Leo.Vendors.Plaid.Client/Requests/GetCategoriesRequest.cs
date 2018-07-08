using Flurl.Http;
using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract]
    [RequestRoute("categories/get")]
    public class GetCategoriesRequest
    {
    }
}