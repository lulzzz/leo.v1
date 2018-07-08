using Leo.Api.Boards;

namespace Leo.Api.Accounts
{
    public class GetCategoriesRequest : IBoardRequest
    {
        public string Search { get; set; }

        public string BoardId { get; set; }

        public string Token { get; set; }

        public string UserId { get; set; }
    }
}