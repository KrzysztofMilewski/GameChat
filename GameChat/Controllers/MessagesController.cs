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

        public MessagesController(IMessageService messageService, IHubContext<MessageHub> hubContext, IHubContext<NotificationHub> asdf)
        {
            _messageService = messageService;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetConversationMessages(int id)
        {
            var currentUserId = User.GetUserId();
            var result = await _messageService.GetMessagesForConversationAsync(id, currentUserId);

            if (!result.Success)
                return BadRequest(result.Message);
            else
                return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ReadMessage(int id)
        {
            var result = await _messageService.ReadMessageAsync(id, User.GetUserId());

            if (!result.Success)
                return BadRequest(result.Message);
            else
                return Ok();
        }
    }
}