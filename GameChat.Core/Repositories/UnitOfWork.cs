using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Persistence;
using System.Threading.Tasks;

namespace GameChat.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IUserRepository UserRepository { get; private set; }
        public IMessageRepository MessageRepository { get; private set; }
        public IConversationRepository ConversationRepository { get; private set; }

        public UnitOfWork(
            ApplicationDbContext context,
            IUserRepository userRepository,
            IMessageRepository messageRepository,
            IConversationRepository conversationRepository)
        {
            _context = context;
            UserRepository = userRepository;
            MessageRepository = messageRepository;
            ConversationRepository = conversationRepository;
        }

        public async Task CompleteTransactionAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
