using Leo.Actors.Interfaces.Boards;
using System.Collections.Generic;

namespace Leo.Api.Boards
{
    public class GetBoardsResponse
    {
        public IEnumerable<Board> Boards { get; set; }
    }
}