using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Persistence;
using System.Threading.Tasks;

namespace GameChat.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _context = context;
            UserRepository = userRepository;
            MessageRepository = messageRepository;
        }

        public IUserRepository UserRepository { get; private set; }

        public IMessageRepository MessageRepository { get; private set; }

        public async Task CompleteTransactionAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
