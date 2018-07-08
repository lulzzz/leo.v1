namespace Leo.Core.Orleans
{
    public abstract class ClusterDecorator : ClusterBuilder
    {
        protected readonly ClusterBuilder _builder;

        public ClusterDecorator(ClusterBuilder builder)
        {
            _builder = builder;
        }
    }
}