using Leo.Api.Boards;

namespace Leo.Api.Accounts
{
    public class PostAccountsTokenRequest : IBoardRequest
    {
        public string BoardId { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string PublicToken { get; set; }

        public string Token { get; set; }

        public string UserId { get; set; }
    }
}