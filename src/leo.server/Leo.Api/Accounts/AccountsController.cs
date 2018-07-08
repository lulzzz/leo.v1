using Leo.Actors.Interfaces.Boards;
using Leo.Core.Id;
using Orleans;
using System.Threading.Tasks;
using System.Web.Http;

namespace Leo.Api.Accounts
{
    [Authorize]
    public class AccountsController : ApiController
    {
        private readonly IClusterClient _cluster;
        private readonly IdProvider _ids;

        public AccountsController(IClusterClient cluster, IdProvider ids)
        {
            _cluster = cluster;
            _ids = ids;
        }

        [HttpGet]
        [Route("boards/{boardId}/accountgroups")]
        public async Task<GetAccountGroupsResponse> Get(GetAccountGroupsRequest request)
        {
            return new GetAccountGroupsResponse
            {
                Groups = await _cluster.GetGrain<IBoardAggregate>(request.BoardId).GetAccountGroupsAsync()
            };
        }

        [HttpPost]
        [Route("{boardId}/accounts/token")]
        public async Task<IHttpActionResult> Post(PostAccountsTokenRequest request)
        {
            await _cluster.GetGrain<IBoardAggregate>(request.BoardId)
                .AddAccounts(new AddAccounts(request.BoardId, request.PublicToken, request.Name, request.Id));

            return StatusCode(System.Net.HttpStatusCode.Accepted);
        }
    }
}