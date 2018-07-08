using Leo.Api.Boards;
using Leo.Core.Api.Interfaces;

namespace Leo.Api.Transactions
{
    public class GetTransactionsRequest : IAuthorizedRequest, IBoardRequest
    {
        public string BoardId { get; set; }

        public string Token { get; set; }

        public string UserId { get; set; }
    }
}