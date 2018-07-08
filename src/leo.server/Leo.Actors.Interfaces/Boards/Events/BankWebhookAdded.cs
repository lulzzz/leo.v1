using Leo.Core.Orleans;
using System;
using System.Runtime.Serialization;

namespace Leo.Actors.Interfaces.Boards
{
    [Serializable]
    [DataContract]
    public class BankWebhookAdded : Event
    {
        public BankWebhookAdded(string bankId, string url, string boardId)
            : base(Streams.Providers.Events, boardId, Streams.Namespaces.Boards)
        {
            BankId = bankId;
            Url = url;
            BoardId = boardId;
        }

        [DataMember]
        public string BankId { get; }

        [DataMember]
        public string BoardId { get; }

        [DataMember]
        public string Url { get; }
    }
}