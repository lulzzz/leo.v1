namespace Leo.Actors.Interfaces.Boards
{
    public class GetEnvelopes
    {
        public GetEnvelopes(params string[] categories)
        {
            Categories = categories ?? new string[] { };
        }

        public string[] Categories { get; }
    }
}