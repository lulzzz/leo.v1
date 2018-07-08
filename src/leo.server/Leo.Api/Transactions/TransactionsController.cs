using Leo.Actors.Interfaces.Boards;
using Leo.Core.Id;
using Orleans;
using System.Threading.Tasks;
using System.Web.Http;

namespace Leo.Api.Transactions
{
    [Authorize]
    public class TransactionsController : ApiController
    {
        private readonly IClusterClient _cluster;
        private readonly IdProvider _ids;

        public TransactionsController(IClusterClient cluster, IdProvider ids)
        {
            _cluster = cluster;
            _ids = ids;
        }

        [HttpGet]
        [Route("boards/{boardid}/transactions")]
        public async Task<GetTransactionsResponse> Get(GetTransactionsRequest request)
        {
            var response =  new GetTransactionsResponse();

            response.TransactionGroups = await _cluster.GetGrain<IBoardAggregate>(request.BoardId).GetTransactionGroupsAsync();

            return response;
        }
    }
}