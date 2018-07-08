using Leo.Core.Api.Interfaces;

namespace Leo.Api.Boards
{
    public interface IBoardRequest : IAuthorizedRequest
    {
        string BoardId { get; set; }
    }
}