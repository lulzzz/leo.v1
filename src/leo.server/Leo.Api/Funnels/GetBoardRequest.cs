using Leo.Core.Api.Interfaces;

namespace Leo.Api.Boards
{
    public class GetBoardRequest : IAuthorizedRequest
    {
        public string Id { get; set; }

        public string Token { get; set; }

        public string UserId { get; set; }
    }
}