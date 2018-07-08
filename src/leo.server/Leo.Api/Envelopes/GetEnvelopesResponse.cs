using Leo.Actors.Interfaces.Boards;
using System.Collections.Generic;

namespace Leo.Api.Envelopes
{
    public class GetEnvelopesResponse
    {
        public IEnumerable<Envelope> Envelopes { get; set; }
    }
}