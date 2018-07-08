using Leo.Core.Orleans;
using System;
using System.Runtime.Serialization;

namespace Leo.Actors.Interfaces.Boards
{
    [Serializable]
    [DataContract]
    public class AccountAdded : Event
    {
        public AccountAdded(string boardId, string accessToken, string accountId,
            string bankName, string bankId, string name, string type, string officialName = null,
            string subType = null, decimal? available = null, decimal? current = null,
            decimal? limit = null)
            : base(Streams.Providers.Events, boardId, Streams.Namespaces.Boards)
        {
            AccessToken = accessToken;
            Available = available;
            Current = current;
            Limit = limit;
            BoardId = boardId;
            AccountId = accountId;
            Name = name;
            OfficialName = officialName;
            SubType = subType;
            Type = type;
            BankName = bankName;
            BankId = bankId;
        }

        [DataMember]
        public string AccessToken { get; private set; }

        [DataMember]
        public string AccountId { get; private set; }

        [DataMember]
        public decimal? Available { get; private set; }

        [DataMember]
        public string BankId { get; private set; }

        [DataMember]
        public string BankName { get; private set; }

        [DataMember]
        public decimal? Current { get; private set; }

        [DataMember]
        public string BoardId { get; private set; }

        [DataMember]
        public decimal? Limit { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string OfficialName { get; private set; }

        [DataMember]
        public string SubType { get; private set; }

        [DataMember]
        public string Type { get; private set; }
    }
}