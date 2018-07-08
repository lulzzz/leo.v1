namespace Leo.Actors.Interfaces.Boards
{
    public class Location
    {
        public Location(string address, string city, Coordinates coordinates, string state, string zip)
        {
            Address = address;
            City = city;
            Coordinates = coordinates;
            State = state;
            Zip = zip;
        }

        public string Address { get; }

        public string City { get; }

        public Coordinates Coordinates { get; }

        public string State { get; }

        public string Zip { get; }
    }
}
