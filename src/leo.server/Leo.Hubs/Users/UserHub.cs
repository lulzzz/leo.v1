using Leo.Actors.Interfaces;
using Leo.Core.Id;
using Leo.Core.Orleans.Streams;
using Microsoft.AspNet.SignalR;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Leo.Hubs.Users
{
    public class UserHub : Hub<IUserClient>
    {
        private readonly IStreamSubscriptionManager _streams;
        private readonly UsersStreamObserver _user;

        public UserHub(IStreamSubscriptionManager streams, UsersStreamObserver user)
        {
            _streams = streams ?? throw new ArgumentNullException(nameof(streams));
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

        public override async Task OnConnected()
        {
            ClaimsPrincipal user = (ClaimsPrincipal)this.Context.User;
            var userIdClaim = user.FindFirst(Leo.Core.Security.ClaimTypes.UserId);

            await Groups.Add(Context.ConnectionId, userIdClaim.Value);
            await _streams.SubscribeAsync(_user, userIdClaim.Value.ToGuid(), Streams.Namespaces.Users, Streams.Providers.Events);

            await base.OnConnected();
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled)
            {
                ClaimsPrincipal user = (ClaimsPrincipal)this.Context.User;
                var userIdClaim = user.FindFirst(Leo.Core.Security.ClaimTypes.UserId);

                await Groups.Remove(Context.ConnectionId, userIdClaim.Value);
                await _streams.UnsubscribeAsync(userIdClaim.Value.ToGuid());
            }

            await base.OnDisconnected(stopCalled);
        }
    }
}