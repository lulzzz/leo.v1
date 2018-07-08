using Orleans;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Leo.Actors.Interfaces.Boards
{
    public interface IBoardAggregate : IGrainWithStringKey
    {
        Task CreateBoard(CreateBoard command);

        Task AddAccounts(AddAccounts command);

        Task AddEnvelope(AddEnvelope command);

        Task PullTransactions(PullTransactions command);

        Task<bool> HasBoard(string userid);

        Task<IEnumerable<AccountGroup>> GetAccountGroupsAsync();

        Task<Board> GetBoardAsync();

        Task<IEnumerable<TransactionGroup>> GetTransactionGroupsAsync();

        Task<IEnumerable<Envelope>> GetEnvelopesAsync();

        Task<IEnumerable<Interfaces.Boards.Category>> SearchCategories(string search);
    }
}