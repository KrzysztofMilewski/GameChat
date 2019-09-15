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

        //TODO temporary
        Task<IEnumerable<User>> GetAll();
    }
}
