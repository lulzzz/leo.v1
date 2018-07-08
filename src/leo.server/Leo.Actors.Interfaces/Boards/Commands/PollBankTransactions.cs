using System;
using System.Runtime.Serialization;

namespace Leo.Actors.Interfaces.Boards
{
    [Serializable]
    [DataContract]
    public class PollBankTransactions
    {
        public PollBankTransactions(string boardId, string bankId)
        {
            BoardId = boardId;
            BankId = bankId;
        }

        [DataMember]
        public string BankId { get; private set; }

        [DataMember]
        public string BoardId { get; private set; }
    }
}