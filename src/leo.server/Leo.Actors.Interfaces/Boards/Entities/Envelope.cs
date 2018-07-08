using System.Linq;

namespace Leo.Actors.Interfaces.Boards
{
    public class Envelope
    {
        public Envelope(string id, string name, string[] categories)
        {
            Id = id;
            Name = name;
            Categories = categories.Select(c => c.ToLower()).ToArray();
        }

        public string[] Categories { get; private set; }

        public string Id { get; internal set; }

        public string Name { get; private set; }
    }
}