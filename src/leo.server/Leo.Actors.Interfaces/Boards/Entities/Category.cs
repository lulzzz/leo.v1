using System.Collections.Generic;

namespace Leo.Actors.Interfaces.Boards
{
    public class Category
    {
        public Category(string categoryId, string group, string[] hierarchy)
        {
            CategoryId = categoryId;
            Group = group;
            Hierarchy = hierarchy;
        }

        public string CategoryId { get; private set; }

        public string Group { get; private set; }

        public string[] Hierarchy { get; private set; }
    }
}