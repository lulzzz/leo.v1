using Leo.Core.Orleans;
using System;
using System.Runtime.Serialization;

namespace Leo.Actors.Interfaces.Boards
{
    [Serializable]
    [DataContract]
    public class BankTransactionsPolled : Event
    {
        public BankTransactionsPolled(string boardId, string bankId, DateTime lastPollDate)
            : base(Streams.Providers.Events, boardId, Streams.Namespaces.Boards)
        {
            BoardId = boardId;
            BankId = bankId;
            LastPollDate = lastPollDate;
        }

        [DataMember]
        public string BankId { get; }

        [DataMember]
        public string BoardId { get; }

        [DataMember]
        public DateTime LastPollDate { get; }
    }
}