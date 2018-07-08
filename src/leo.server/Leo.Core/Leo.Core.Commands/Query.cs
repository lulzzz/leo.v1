using System;
using System.Runtime.Serialization;

namespace Leo.Core.Commands
{
    [Serializable]
    [DataContract]
    public class Query<TResult> : IQuery<TResult>
    {
        public Query(int maxAttempts = 1, Guid? transactionId = null)
        {
            AttemptsRemaining = maxAttempts;
            TransactionId = transactionId;
        }

        [DataMember]
        public int AttemptsRemaining { get; set; }

        [DataMember]
        public Guid? TransactionId { get; private set; }
    }
}