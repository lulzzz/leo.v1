using Leo.Core.Api.Interfaces;

namespace Leo.Api.Boards
{
    public class GetBoardsRequest : IAuthorizedRequest
    {
        public string UserId { get; set; }

        public string Token { get; set; }
    }
}