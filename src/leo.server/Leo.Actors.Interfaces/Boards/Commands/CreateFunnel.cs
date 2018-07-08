using System;
using System.Runtime.Serialization;

namespace Leo.Actors.Interfaces.Boards
{
    [Serializable]
    [DataContract]
    public class CreateBoard
    {
        public CreateBoard(string boardId, string name, string userid)
        {
            BoardId = boardId;
            Name = name;
            UserId = userid;
        }

        [DataMember]
        public string BoardId { get; }

        [DataMember]
        public string Name { get; }

        [DataMember]
        public string UserId { get; }
    }
}