using GameChat.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameChat.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> FindByUsernameAsync(string username);
        Task<User> FindById(int id);

        Task<IEnumerable<User>> GetUsers(string filter);
    }
}
