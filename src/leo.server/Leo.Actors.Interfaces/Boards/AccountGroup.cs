using System.Collections.Generic;

namespace Leo.Actors.Interfaces.Boards
{
    public class AccountGroup
    {
        public IEnumerable<AccountSummary> Accounts { get; set; }

        public string Name { get; set; }

        public decimal Total { get; set; }
    }
}