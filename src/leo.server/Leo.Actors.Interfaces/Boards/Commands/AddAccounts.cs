using System;
using System.Runtime.Serialization;

namespace Leo.Actors.Interfaces.Boards
{
    [Serializable]
    [DataContract]
    public class AddAccounts
    {
        public AddAccounts(string boardId, string publicToken, string bankName, string bankId)
        {
            BoardId = boardId;
            PublicToken = publicToken;
            BankName = bankName;
            BankId = bankId;
        }

        [DataMember]
        public string BoardId { get; private set; }

        [DataMember]
        public string BankId { get; private set; }

        [DataMember]
        public string BankName { get; private set; }

        [DataMember]
        public string PublicToken { get; private set; }
    }
}
