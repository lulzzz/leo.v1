namespace Leo.Core.Orleans.Client
{
    public abstract class ClientConfigurationDecorator : ClientConfigurationBuilder
    {
        protected readonly ClientConfigurationBuilder _builder;

        public ClientConfigurationDecorator(ClientConfigurationBuilder builder)
        {
            _builder = builder;
        }
    }
}