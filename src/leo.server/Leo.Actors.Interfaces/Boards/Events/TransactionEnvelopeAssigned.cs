using Leo.Core.Orleans;

namespace Leo.Actors.Interfaces.Boards
{
    public class TransactionEnvelopeAssigned : Event
    {
        public TransactionEnvelopeAssigned(string boardId, string transactionId, string accountId, string matchedEnvelopeId)
            : base(Streams.Providers.Events, boardId, Streams.Namespaces.Boards)
        {
            TransactionId = transactionId;
            AccountId = accountId;
            MatchedEnvelopeId = matchedEnvelopeId;
        }

        public string AccountId { get; }

        public string MatchedEnvelopeId { get; }

        public string TransactionId { get; }
    }
}