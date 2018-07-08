using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract]
    public class Item
    {
        public Item(IEnumerable<Products> availableProducts, IEnumerable<Products> billedProducts, ItemError error, string institutionId, string itemId, string webhook)
        {
            AvailableProducts = availableProducts;
            BilledProducts = billedProducts;
            Error = error;
            InstitutionId = institutionId;
            ItemId = itemId;
            Webhook = webhook;
        }

        [DataMember(Name = "available_products")]
        public IEnumerable<Products> AvailableProducts { get; private set; }

        [DataMember(Name = "billed_products")]
        public IEnumerable<Products> BilledProducts { get; private set; }

        [DataMember(Name = "error")]
        public ItemError Error { get; private set; }

        [DataMember(Name = "institution_id")]
        public string InstitutionId { get; private set; }

        [DataMember(Name = "item_id")]
        public string ItemId { get; private set; }

        [DataMember(Name = "webhook")]
        public string Webhook { get; private set; }
    }
}