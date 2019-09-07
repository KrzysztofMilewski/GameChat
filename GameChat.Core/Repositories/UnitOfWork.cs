using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Persistence;
using System.Threading.Tasks;

namespace GameChat.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            UserRepository = userRepository;
        }

        public IUserRepository UserRepository { get; private set; }

        public async Task CompleteTransactionAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
