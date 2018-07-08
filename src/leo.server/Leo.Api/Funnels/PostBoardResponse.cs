namespace Leo.Api.Boards
{
    public class PostBoardResponse
    {
        public PostBoardResponse(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}