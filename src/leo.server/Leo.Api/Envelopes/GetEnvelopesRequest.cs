using Leo.Api.Boards;

namespace Leo.Api.Envelopes
{
    public class GetEnvelopesRequest : IBoardRequest
    {
        public string BoardId { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}