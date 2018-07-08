using System;
using System.Runtime.Serialization;

namespace Leo.Actors.Interfaces.Boards
{
    [Serializable]
    [DataContract]
    public class GetAccounts
    {
        public GetAccounts(string boardId, string accessToken, string bankId, string bankName)
        {
            BoardId = boardId;
            AccessToken = accessToken;
            BankId = bankId;
            BankName = bankName;
        }

        [DataMember]
        public string AccessToken { get; private set; }

        [DataMember]
        public string BankId { get; private set; }

        [DataMember]
        public string BankName { get; private set; }

        [DataMember]
        public string BoardId { get; private set; }
    }
}
