using Leo.Actors.Interfaces;
using Leo.Core.Id;
using Leo.Core.Orleans.Streams;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Leo.Hubs.Boards
{
    public class BoardHub : Hub<IBoardClient>
    {
        private readonly IStreamSubscriptionManager _streams;
        private readonly BoardStreamObserver _board;

        public BoardHub(IStreamSubscriptionManager streams, BoardStreamObserver board)
        {
            _streams = streams ?? throw new ArgumentNullException(nameof(streams));
            _board = board ?? throw new ArgumentNullException(nameof(board));
        }

        public override async Task OnConnected()
        {
            var boardId = this.Context.Request.QueryString["boardid"];
            if (string.IsNullOrEmpty(boardId))
                throw new ArgumentNullException("boardid", "Board identifier must be provided.");
            
            await Groups.Add(Context.ConnectionId, boardId);
            await _streams.SubscribeAsync(_board, boardId.ToGuid(), Streams.Namespaces.Boards, Streams.Providers.Events);

            await base.OnConnected();
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled)
            {
                var boardId = this.Context.Request.QueryString["boardid"];
                if (string.IsNullOrEmpty(boardId))
                    throw new ArgumentNullException("boardid", "Board identifier must be provided.");

                //todo: this may not be what is wanted if it unsubscribes all users from the board events.
                await Groups.Remove(Context.ConnectionId, boardId);
                await _streams.UnsubscribeAsync(boardId.ToGuid());
            }

            await base.OnDisconnected(stopCalled);
        }
    }
}
