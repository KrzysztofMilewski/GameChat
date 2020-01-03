using GameChat.Core.DTOs;
using GameChat.Core.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameChat.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<ServiceResult<string>> AuthenticateAsync(UserLoginRegisterDto user);
        Task<ServiceResult> CreateNewAccountAsync(UserLoginRegisterDto user);
        Task<ServiceResult<IEnumerable<UserDto>>> GetUsers(string filter);
        Task<ServiceResult<UserDto>> GetUserById(int id);
    }
}
