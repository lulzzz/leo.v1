using Leo.Actors.Interfaces.Boards;
using Leo.Actors.Interfaces.Users;
using Leo.Core.Id;
using Orleans;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Leo.Api.Boards
{
    [Authorize]
    public class BoardsController : ApiController
    {
        private readonly IClusterClient _cluster;
        private readonly IdProvider _ids;

        public BoardsController(IClusterClient cluster, IdProvider ids)
        {
            _cluster = cluster;
            _ids = ids;
        }

        [HttpGet]
        [Route("boards/{id}", Name = "getboard")]
        public async Task<GetBoardResponse> Get(GetBoardRequest request)
        {
            GetBoardResponse rvalue = new GetBoardResponse();

            rvalue.Board = await _cluster.GetGrain<IBoardAggregate>(request.Id).GetBoardAsync();

            return rvalue;
        }

        [HttpGet]
        [Route("boards", Name = "getboards")]
        public async Task<GetBoardsResponse> Get(GetBoardsRequest request)
        {
            var response = new GetBoardsResponse();
            var user = _cluster.GetGrain<IUserAggregate>(request.UserId);
            response.Boards = await user.GetBoardsAsync().ConfigureAwait(false);

            return response;
        }

        [HttpPost]
        [Route("boards", Name = "postboard")]
        public async Task<PostBoardResponse> Post(PostBoardRequest request)
        {
            var boardId = _ids.Create();
            var command = new CreateBoard(boardId, request.Name, request.UserId);
            var user = _cluster.GetGrain<IUserAggregate>(request.UserId);
            await user.CreateBoard(command);            

            return new PostBoardResponse(boardId);
        }
    }
}