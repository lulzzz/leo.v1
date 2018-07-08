using Orleans;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Threading;

namespace Leo.Api
{
    public class ClusterInitializationFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            IClusterClient cluster = actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(IClusterClient)) as IClusterClient;

            if (cluster?.IsInitialized == false)
                await cluster.Connect();

            await base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
    }
}