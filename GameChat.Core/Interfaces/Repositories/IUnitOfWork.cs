using System.Threading.Tasks;

namespace GameChat.Core.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IMessageRepository MessageRepository { get; }

        Task CompleteTransactionAsync();
    }
}
