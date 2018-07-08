using Leo.Actors.Interfaces.Boards;
using Leo.Actors.Interfaces.Users;
using Leo.Core.Orleans.EventSourcing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leo.Actors.Users
{
    public class UserAggregate : EventSourcedGrain<UserAggregateState>, IUserAggregate
    {
        public async Task CreateBoard(CreateBoard command)
        {
            var board = GrainFactory.GetGrain<IBoardAggregate>(command.BoardId);
            await board.CreateBoard(command);

            var @event = new UserAddedToBoard(command.BoardId, command.Name, command.UserId);
            RaiseEvent(@event);
        }

        public Task<IEnumerable<Board>> GetBoardsAsync()
        {
            IEnumerable<Board> rvalues = State.Boards.Select(f => f.Value).ToList();

            return Task.FromResult(rvalues);
        }

        public Task LoginUser(LoginUser message)
        {
            var @event = new UserLoggedIn(message.UserId, message.AuthenticationType, message.ExternalId, message.Name, message.Email);
            RaiseEvent(@event);

            return Task.CompletedTask;
        }
    }
}