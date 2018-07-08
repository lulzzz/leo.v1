using Leo.Actors.Interfaces.Boards;
using Leo.Core.Id;
using Orleans;
using System.Threading.Tasks;
using System.Web.Http;

namespace Leo.Api.Accounts
{
    [Authorize]
    public class CategoriesController : ApiController
    {
        private readonly IClusterClient _cluster;
        private readonly IdProvider _ids;

        public CategoriesController(IClusterClient cluster, IdProvider ids)
        {
            _cluster = cluster;
            _ids = ids;
        }

        [HttpGet]
        [Route("boards/{boardId}/categories")]
        public async Task<GetCategoriesResponse> Get(GetCategoriesRequest request)
        {
            return new GetCategoriesResponse
            {
                Categories = await _cluster.GetGrain<IBoardAggregate>(request.BoardId).SearchCategories(request.Search)
            };
        }
    }
}