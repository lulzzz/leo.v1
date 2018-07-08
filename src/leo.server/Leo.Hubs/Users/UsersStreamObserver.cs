using Leo.Actors.Interfaces.Users;
using Leo.Core.Orleans.Streams;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace Leo.Hubs.Users
{
    public class UsersStreamObserver : StreamObserver
    {
        private readonly IHubConnectionContext<IUserClient> _clients;

        public UsersStreamObserver(IHubConnectionContext<IUserClient> clients)
        {
            _clients = clients;
        }

        public Task OnNextAsync(UserAddedToBoard @event)
        {
            _clients.Group(@event.UserId)?.BoardAdded(@event);
            return Task.CompletedTask;
        }
    }
}