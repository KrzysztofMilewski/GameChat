using GameChat.Core.DTOs;
using GameChat.Core.Helpers;
using System.Threading.Tasks;

namespace GameChat.Core.Interfaces.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Authenticates user and returns security token serialised to string if authentication was successful
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> AuthenticateAsync(UserLoginRegisterDto user);
        Task<ServiceResult> CreateNewAccountAsync(UserLoginRegisterDto user);
    }
}
