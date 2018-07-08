using Leo.Actors.Interfaces.Boards;
using Orleans;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Leo.Api.Boards
{
    public class BoardAuthorizationFilter : ActionFilterAttribute
    {
        private IClusterClient _cluster;

        public BoardAuthorizationFilter(IClusterClient cluster)
        {
            _cluster = cluster;
        }

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var boardRequestArg = actionContext.ActionArguments.FirstOrDefault(arg => arg.Value is IBoardRequest);
            if (boardRequestArg.Value != null)
            {
                if (!_cluster.IsInitialized)
                    await _cluster.Connect();

                var boardRequest = boardRequestArg.Value as IBoardRequest;
                var hasBoard = await _cluster.GetGrain<IBoardAggregate>(boardRequest.BoardId).HasBoard(boardRequest.UserId);
                if (!hasBoard)
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}