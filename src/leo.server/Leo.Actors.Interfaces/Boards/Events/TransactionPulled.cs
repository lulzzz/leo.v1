using Leo.Core.Orleans;
using System;
using System.Runtime.Serialization;

namespace Leo.Actors.Interfaces.Boards
{
    [Serializable]
    [DataContract]
    public class TransactionPulled : Event
    {
        public TransactionPulled(string transactionId, string boardId, string accountId,
            string transactionType, decimal amount, DateTime date, string categoryId, string[] categories,
            decimal? latitude, decimal? longitude,
            string address, string city, string state, string zip,
            string payee, string ppdid, string referenceNumber)
            : base(Streams.Providers.Events, boardId, Streams.Namespaces.Boards)
        {
            TransactionId = transactionId;
            BoardId = boardId;
            AccountId = accountId;
            TransactionType = transactionType;
            Amount = amount;
            Date = date;
            CategoryId = categoryId;
            Categories = categories;
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            Payee = payee;
            PpdId = ppdid;
            ReferenceNumber = referenceNumber;
        }

        [DataMember]
        public string AccountId { get; private set; }

        [DataMember]
        public string Address { get; private set; }

        [DataMember]
        public decimal Amount { get; private set; }

        [DataMember]
        public string BoardId { get; private set; }

        [DataMember]
        public string[] Categories { get; private set; }

        [DataMember]
        public string CategoryId { get; }

        [DataMember]
        public string City { get; private set; }

        [DataMember]
        public DateTime Date { get; private set; }

        [DataMember]
        public decimal? Latitude { get; private set; }

        [DataMember]
        public decimal? Longitude { get; private set; }

        [DataMember]
        public string Payee { get; private set; }

        [DataMember]
        public string PpdId { get; private set; }

        [DataMember]
        public string ReferenceNumber { get; private set; }

        [DataMember]
        public string State { get; private set; }

        [DataMember]
        public string TransactionId { get; private set; }

        [DataMember]
        public string TransactionType { get; private set; }

        [DataMember]
        public string Zip { get; private set; }
    }
}