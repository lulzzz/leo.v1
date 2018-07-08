using System;
using System.Runtime.Serialization;

namespace Leo.Actors.Interfaces.Boards
{
    [Serializable]
    [DataContract]
    public class PullTransactions
    {
        public PullTransactions(string boardId, string itemId, DateTime start, DateTime end)
        {
            BoardId = boardId;
            ItemId = itemId;
            Start = start;
            End = end;
        }

        [DataMember]
        public string BoardId { get; private set; }

        [DataMember]
        public DateTime End { get; }

        [DataMember]
        public string ItemId { get; private set; }

        [DataMember]
        public int NewTransactions { get; private set; }

        [DataMember]
        public DateTime Start { get; }
    }
}