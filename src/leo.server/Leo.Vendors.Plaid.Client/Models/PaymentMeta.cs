using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract(Name = "payment_meta")]
    public class PaymentMeta
    {
        [DataMember(Name = "by_order_of")]
        public string ByOrderOf { get; set; }

        [DataMember(Name = "payee")]
        public string Payee { get; set; }

        [DataMember(Name = "payer")]
        public string Payer { get; set; }

        [DataMember(Name = "payment_method")]
        public string PaymentMethod { get; set; }

        [DataMember(Name = "payment_processor")]
        public string PaymentProcessor { get; set; }

        [DataMember(Name = "ppd_id")]
        public string PpdId { get; set; }

        [DataMember(Name = "reason")]
        public string Reason { get; set; }

        [DataMember(Name = "reference_number")]
        public string ReferenceNumber { get; set; }
    }
}