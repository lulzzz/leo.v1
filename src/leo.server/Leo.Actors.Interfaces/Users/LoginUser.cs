using System;
using System.Runtime.Serialization;

namespace Leo.Actors.Interfaces.Users
{
    [Serializable]
    [DataContract]
    public class LoginUser
    {
        public LoginUser(string commandId, string userId, string authenticationType, string externalId, string name, string email, string parentCommandId = null)
        {
            UserId = userId;
            AuthenticationType = authenticationType;
            ExternalId = externalId;
            Name = name;
            Email = email;
        }

        [DataMember]
        public string AuthenticationType { get; private set; }

        [DataMember]
        public string Email { get; private set; }

        [DataMember]
        public string ExternalId { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string UserId { get; private set; }
    }
}