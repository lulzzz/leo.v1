namespace Leo.Actors.Interfaces.Boards
{
    public class Coordinates
    {
        public Coordinates(decimal? latitude, decimal? longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public decimal? Latitude { get; }

        public decimal? Longitude { get; }
    }
}
