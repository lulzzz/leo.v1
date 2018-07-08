using Leo.Core.Orleans;
using System;
using System.Runtime.Serialization;

namespace Leo.Actors.Interfaces.Boards
{
    [Serializable]
    [DataContract]
    public class BoardCreated : Event
    {
        public BoardCreated(string boardName, string boardId, string userid)
            : base(Streams.Providers.Events, userid, Streams.Namespaces.Boards)
        {
            BoardName = boardName;
            BoardId = boardId;
            UserId = userid;
        }

        [DataMember]
        public string BoardId { get; private set; }

        [DataMember]
        public string BoardName { get; private set; }

        [DataMember]
        public string UserId { get; private set; }
    }
}