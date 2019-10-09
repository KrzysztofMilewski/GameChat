using GameChat.Core.DTOs;
using GameChat.Core.Interfaces.Services;
using GameChat.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameChat.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConversationsController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationsController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConversation(ConversationDto conversation)
        {
            conversation.Participants.Add(new UserDto() { Id = User.GetUserId() });
            var result = await _conversationService.CreateNewConversation(conversation);

            if (!result.Success)
                return BadRequest(result.Message);
            else
                return Ok(new { conversationId = result.Data });
        }

        [HttpGet]
        public async Task<IActionResult> GetConversationsForUser()
        {
            var result = await _conversationService.GetConversationsForUser(User.GetUserId());

            if (result.Success)
                return Ok(result.Data);
            else
                return BadRequest(result.Message);
        }
    }
}