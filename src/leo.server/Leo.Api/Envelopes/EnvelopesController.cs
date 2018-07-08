using Leo.Actors.Interfaces.Boards;
using Leo.Core.Id;
using Orleans;
using System.Threading.Tasks;
using System.Web.Http;

namespace Leo.Api.Envelopes
{
    public class EnvelopesController : ApiController
    {
        private readonly IClusterClient _cluster;
        private readonly IdProvider _ids;

        public EnvelopesController(IClusterClient cluster, IdProvider ids)
        {
            _cluster = cluster;
            _ids = ids;
        }

        [HttpGet]
        [Route("boards/{boardId}/envelopes", Name = "getenvelopes")]
        public async Task<GetEnvelopesResponse> Get(GetEnvelopesRequest request)
        {
            GetEnvelopesResponse response = new GetEnvelopesResponse();
            response.Envelopes = await _cluster.GetGrain<IBoardAggregate>(request.BoardId).GetEnvelopesAsync();
            return response;
        }

        [HttpPost]
        [Route("boards/{boardId}/envelopes", Name = "postenvelope")]
        public async Task<PostEnvelopeResponse> Post(PostEnvelopeRequest request)
        {
            PostEnvelopeResponse response = new PostEnvelopeResponse();
            response.Id = _ids.Create();

            await _cluster.GetGrain<IBoardAggregate>(request.BoardId)
                .AddEnvelope(new AddEnvelope(response.Id, request.Name, request.Categories, request.BoardId, request.UserId));

            return response;
        }
    }
}