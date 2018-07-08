using Leo.Actors.Interfaces.Boards;
using Leo.Core.Id;
using Orleans;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Leo.Api.Webhooks
{
    public class WebhooksController : ApiController
    {
        private readonly IClusterClient _cluster;
        private readonly IdProvider _ids;

        public WebhooksController(IClusterClient cluster, IdProvider ids)
        {
            _cluster = cluster;
            _ids = ids;
        }

        [HttpPost]
        [Route("boards/{boardid}/webhook")]
        public async Task<IHttpActionResult> Post(PostBoardWebhookRequest request)
        {
            if(request.WebhookType == Vendors.Plaid.Client.WebhookTypes.Transactions && request.WebhookCode == Vendors.Plaid.Client.WebhookCodes.INITIAL_UPDATE)
            {
                await _cluster.GetGrain<IBoardAggregate>(request.BoardId).PullTransactions(
                    new PullTransactions(request.BoardId, request.ItemId, DateTime.Today.AddDays(-30), DateTime.Today)
                );
            }
            else if(request.WebhookType == Vendors.Plaid.Client.WebhookTypes.Transactions && request.WebhookCode == Vendors.Plaid.Client.WebhookCodes.HISTORICAL_UPDATE)
            {

            }

            //todo: update board aggregate with webhook status...
            return Ok();
        }
    }
}