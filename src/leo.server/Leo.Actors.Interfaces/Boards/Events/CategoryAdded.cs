using Leo.Core.Orleans;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Leo.Actors.Interfaces.Boards
{
    [Serializable]
    [DataContract]
    public class CategoryAdded : Event
    {
        public CategoryAdded(string boardId, string categoryId, string group, string[] hierarchy)
            : base(Streams.Providers.Events, boardId, Streams.Namespaces.Boards)
        {
            BoardId = boardId;
            CategoryId = categoryId;
            Group = group;
            Hierarchy = hierarchy;
        }

        public string BoardId { get; private set; }

        public string CategoryId { get; private set; }

        public string Group { get; private set; }

        public string[] Hierarchy { get; private set; }
    }
}