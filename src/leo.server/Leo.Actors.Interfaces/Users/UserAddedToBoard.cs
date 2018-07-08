using Leo.Core.Orleans;
using System;
using System.Runtime.Serialization;

namespace Leo.Actors.Interfaces.Users
{
    [Serializable]
    [DataContract]
    public class UserAddedToBoard : Event
    {
        public UserAddedToBoard(string id, string name, string userId)
            : base(Streams.Providers.Events, userId, Streams.Namespaces.Users)
        {
            Id = id;
            Name = name;
            UserId = userId;
        }

        [DataMember]
        public string Id { get; private set; }
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string UserId { get; private set; }
    }
}