using Leo.Actors.Interfaces.Users;

namespace Leo.Hubs.Users
{
    public interface IUserClient
    {
        void BoardAdded(UserAddedToBoard @event);
    }
}