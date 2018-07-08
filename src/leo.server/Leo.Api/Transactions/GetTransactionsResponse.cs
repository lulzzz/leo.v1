using Leo.Actors.Interfaces.Boards;
using System.Collections.Generic;

namespace Leo.Api.Transactions
{
    public class GetTransactionsResponse
    {
        public IEnumerable<TransactionGroup> TransactionGroups { get; set; }
    }
}