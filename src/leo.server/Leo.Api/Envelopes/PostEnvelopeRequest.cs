using Leo.Api.Boards;

namespace Leo.Api.Envelopes
{
    public class PostEnvelopeRequest : IBoardRequest
    {
        public string[] Categories { get; set; }

        public string BoardId { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }

        public string UserId { get; set; }
    }
}