using Leo.Actors.Interfaces.Boards;
using Leo.Actors.Interfaces.Users;
using Leo.Core.Orleans.EventSourcing;
using System.Collections.Generic;

namespace Leo.Actors.Users
{
    public class UserAggregateState : AggregateRoot
    {
        public string AuthenticationType { get; private set; }

        public string Email { get; private set; }

        public string ExternalId { get; private set; }

        public IDictionary<string, Board> Boards { get; private set; } = new Dictionary<string, Board>();

        public string Name { get; private set; }

        public void Apply(UserLoggedIn @event)
        {
            AuthenticationType = @event.AuthenticationType;
            Email = @event.Email;
            ExternalId = @event.ExternalId;
            Name = @event.Name;
        }

        public void Apply(UserAddedToBoard @event)
        {
            Boards[@event.Id] = new Board { Id = @event.Id, Name = @event.Name };
        }
    }
}