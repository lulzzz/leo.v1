using System;

namespace Leo.Actors.Interfaces.Boards
{
    public class Account
    {
        public Account(string id, string name, string officialName, string type, string subType, string accessToken, string bankId, string bankName, DateTime createdOn, AccountBalance balance)
        {
            BankName = bankName;
            BankId = bankId;
            AccessToken = accessToken;
            CreatedOn = createdOn;
            Id = id;
            Name = name;
            OfficialName = officialName;
            SubType = subType;
            Type = type;
            Balance = balance;
        }

        public string AccessToken { get; private set; }

        public AccountBalance Balance { get; private set; }

        public string BankId { get; private set; }

        public string BankName { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public string OfficialName { get; private set; }

        public string SubType { get; private set; }

        public string Type { get; private set; }
    }
}