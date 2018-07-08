using Leo.Actors.Interfaces;
using Leo.Core.Orleans;
using System;

namespace Leo.Actors.Interfaces.Boards
{
    public class EnvelopeAdded : Event
    {
        public EnvelopeAdded(string id, string name, string[] categories, string boardId, string userId)
            : base(Streams.Providers.Events, boardId, Streams.Namespaces.Boards)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Categories = categories ?? throw new ArgumentNullException(nameof(categories));
            BoardId = boardId ?? throw new ArgumentNullException(nameof(boardId));
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
        }

        public string Id { get; }
        public string Name { get; }
        public string[] Categories { get; }
        public string BoardId { get; }
        public string UserId { get; }
    }
}
