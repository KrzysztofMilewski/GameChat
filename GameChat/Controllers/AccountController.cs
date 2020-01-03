using GameChat.Core.DTOs;
using GameChat.Core.Interfaces.Services;
using GameChat.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameChat.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount([FromBody] UserLoginRegisterDto userDto)
        {
            var result = await _userService.CreateNewAccountAsync(userDto);

            if (result.Success)
                return Ok(new { result.Message });
            else
                return BadRequest(result.Message);
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginRegisterDto userDto)
        {
            var result = await _userService.AuthenticateAsync(userDto);

            if (result.Success)
                return Ok(new { token = result.Data });
            else
                return Unauthorized(result.Data);
        }

        [HttpGet("current")]
        public IActionResult GetCurrentUser()
        {
            var currentUser = new UserDto()
            {
                Id = User.GetUserId(),
                Username = User.Identity.Name
            };

            return Ok(currentUser);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetUsers([FromQuery]string username)
        {
            var users = await _userService.GetUsers(filter: username);
            return Ok(users.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _userService.GetUserById(id);

            if (result.Success)
                return Ok(result.Data);
            else
                return NotFound();
        }
    }
}