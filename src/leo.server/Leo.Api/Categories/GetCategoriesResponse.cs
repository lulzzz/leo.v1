using Leo.Actors.Interfaces.Boards;
using System.Collections.Generic;

namespace Leo.Api.Accounts
{
    public class GetCategoriesResponse
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}