using GameChat.Core.DTOs;
using GameChat.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
        public async Task<IActionResult> Authenticate([FromBody]UserLoginRegisterDto userDto)
        {
            var result = await _userService.AuthenticateAsync(userDto);

            if (result.Success)
                return Ok(new { token = result.Data });
            else
                return Unauthorized(result.Data);
        }

        [HttpGet("private")]
        public IActionResult Private()
        {
            var numbers = Enumerable.Range(1, 20);
            return Ok(numbers);
        }

        [AllowAnonymous]
        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok("Whatever");
        }
    }
}