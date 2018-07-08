namespace Leo.Actors.Interfaces.Boards
{
    public class AccountBalance
    {
        public AccountBalance(decimal? available, decimal? current, decimal? limit)
        {
            Available = available;
            Current = current;
            Limit = limit;
        }

        public decimal? Available { get; }

        public decimal? Current { get; }

        public decimal? Limit { get; }
    }
}