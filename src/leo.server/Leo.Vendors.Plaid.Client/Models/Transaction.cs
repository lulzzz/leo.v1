using System;
using System.Runtime.Serialization;

namespace Leo.Vendors.Plaid.Client
{
    [DataContract(Name = "transaction")]
    public class Transaction
    {
        [DataMember(Name = "account_id")]
        public string AccountId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "account_owner")]
        public string AccountOwner { get; set; }

        [DataMember(Name = "amount")]
        public decimal Amount { get; set; }

        [DataMember(Name = "category")]
        public string[] Category { get; set; }

        [DataMember(Name = "category_id")]
        public string CategoryId { get; set; }

        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "payment_meta")]
        public PaymentMeta PaymentMeta { get; set; }

        [DataMember(Name = "pending")]
        public bool Pending { get; set; }

        [DataMember(Name = "pending_transaction_id")]
        public string PendingTransactionId { get; set; }

        [DataMember(Name = "transaction_id")]
        public string TransactionId { get; set; }

        [DataMember(Name = "transaction_type")]
        public string TransactionType { get; set; }
    }
}