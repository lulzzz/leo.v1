using Leo.Core.Orleans;
using System;
using System.Runtime.Serialization;

namespace Leo.Actors.Interfaces.Boards
{
    [Serializable]
    [DataContract]
    public class BankAdded : Event
    {
        public BankAdded(string id, string name, string accessToken, string boardId)
            : base(Streams.Providers.Events, boardId, Streams.Namespaces.Boards)
        {
            Id = id;
            Name = name;
            AccessToken = accessToken;
            BoardId = boardId;
        }

        [DataMember]
        public string AccessToken { get; private set; }

        [DataMember]
        public string BoardId { get; private set; }

        [DataMember]
        public string Id { get; private set; }

        [DataMember]
        public string Name { get; private set; }
    }
}