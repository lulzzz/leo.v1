using Leo.Actors.Interfaces.Boards;
using System.Collections.Generic;

namespace Leo.Api.Accounts
{
    public class GetAccountGroupsResponse
    {
        public IEnumerable<AccountGroup> Groups { get; set; }
    }
}