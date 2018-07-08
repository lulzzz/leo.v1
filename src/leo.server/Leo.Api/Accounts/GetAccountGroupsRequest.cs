using Leo.Api.Boards;

namespace Leo.Api.Accounts
{
    public class GetAccountGroupsRequest : IBoardRequest
    {
        public string BoardId { get; set; }

        public string Token { get; set; }

        public string UserId { get; set; }
    }
}