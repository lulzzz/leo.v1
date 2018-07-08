using Leo.Core.Id;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Orleans.Streams
{
    public static class StreamProviderExtensions
    {
        public static IAsyncStream<T> GetStream<T>(this IStreamProvider streamProvider, string streamId, string streamNamespace)
        {
            return streamProvider.GetStream<T>(streamId.ToGuid(), streamNamespace);
        }
    }
}