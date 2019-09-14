using GameChat.Core.DTOs;
using GameChat.Core.Interfaces.Services;
using GameChat.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace GameChat.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetConversationMessages(int id)
        {
            return Ok(await _messageService.GetMessagesForConversation(id));
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody]MessageDto message)
        {
            message.SenderId = User.GetUserId();

            await _messageService.SendMessage(message);
            return Ok();
        }
    }
}