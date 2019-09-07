using System.Threading.Tasks;

namespace GameChat.Core.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        Task CompleteTransactionAsync();
    }
}
