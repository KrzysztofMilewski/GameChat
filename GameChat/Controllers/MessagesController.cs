using GameChat.Core.DTOs;
using GameChat.Core.Interfaces.Services;
using GameChat.Web.Hubs;
using GameChat.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GameChat.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IHubContext<MessageHub> _hubContext;

        public MessagesController(IMessageService messageService, IHubContext<MessageHub> hubContext)
        {
            _messageService = messageService;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetConversationMessages(int id)
        {
            var currentUserId = User.GetUserId();
            var result = await _messageService.GetMessagesForConversation(id, currentUserId);

            if (!result.Success)
                return BadRequest(result.Message);
            else
                return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody]MessageDto message)
        {
            message.SenderId = User.GetUserId();

            var result = await _messageService.SendMessage(message);

            if (!result.Success)
                return BadRequest(result.Message);
            else
            {
                //TODO change return type to saved message and pass through the hub
                await _hubContext.Clients.All.SendAsync("SendMessage", message);
                return Ok();
            }
        }
    }
}