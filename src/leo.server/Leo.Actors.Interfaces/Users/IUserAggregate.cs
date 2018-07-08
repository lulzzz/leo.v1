using Leo.Actors.Interfaces.Boards;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leo.Actors.Interfaces.Users
{
    public interface IUserAggregate : IGrainWithStringKey
    {
        Task LoginUser(LoginUser message);

        Task CreateBoard(CreateBoard command);

        Task<IEnumerable<Board>> GetBoardsAsync();
    }
}