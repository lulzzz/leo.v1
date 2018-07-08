using System;

namespace Leo.Actors.Interfaces.Boards
{
    public class AddEnvelope
    {
        public AddEnvelope(string id, string name, string[] categories, string boardId, string userId)
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
