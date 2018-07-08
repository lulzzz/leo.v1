using System;

namespace Leo.Actors.Interfaces.Boards
{
    public class Transaction
    {
        public Transaction(string id, string accountId, decimal amount, DateTime transactionDate, string type, string categoryId, DateTime createDate, Location location, PaymentMeta paymentMeta)
        {
            Id = id;
            AccountId = accountId;
            Amount = amount;
            TransactionDate = transactionDate;
            Type = type;
            CategoryId = categoryId;
            CreateDate = createDate;
            Location = location;
            PaymentMeta = paymentMeta;
        }

        public string AccountId { get; }

        public decimal Amount { get; private set; }

        public string CategoryId { get; }

        public DateTime CreateDate { get; private set; }

        public string EnvelopeId { get; private set; }

        public string Id { get; private set; }

        public Location Location { get; private set; }

        public PaymentMeta PaymentMeta { get; private set; }

        public DateTime TransactionDate { get; private set; }

        public string Type { get; private set; }

        public void AssignEnvelope(string envelopeId)
        {
            EnvelopeId = envelopeId;
        }
    }
}