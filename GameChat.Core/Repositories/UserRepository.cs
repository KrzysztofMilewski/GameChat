using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Models;
using GameChat.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameChat.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _users;

        public UserRepository(ApplicationDbContext context)
        {
            _users = context.Set<User>();
        }

        public async Task AddUserAsync(User user)
        {
            await _users.AddAsync(user);
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> FindById(int id)
        {
            return await _users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
