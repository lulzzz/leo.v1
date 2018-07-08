using Leo.Core.Api.Interfaces;
using System.Runtime.Serialization;

namespace Leo.Api.Boards
{
    public class PostBoardRequest : IAuthorizedRequest
    {
        public string Name { get; set; }

        public string Token { get; set; }

        public string UserId { get; set; }
    }
}