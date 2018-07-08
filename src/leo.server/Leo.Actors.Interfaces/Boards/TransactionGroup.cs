using System;
using System.Collections.Generic;

namespace Leo.Actors.Interfaces.Boards
{
    public class TransactionGroup
    {
        public DateTime TransactionDate { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}