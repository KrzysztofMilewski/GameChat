using GameChat.Core.Interfaces.Services;
using GameChat.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GameChat.Web.Hubs
{
    [Authorize]
    public class ConversationHub : Hub
    {
        private readonly IConversationService _conversationService;

        public ConversationHub(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        public async Task GetConversationsForUser()
        {
            var result = await _conversationService.GetConversationsForUser(Context.User.GetUserId());

            if (result.Success)
            {
                await Clients.Caller.SendAsync("InitialLoadOfConversations", result.Data);
            }
            else
                await Clients.Caller.SendAsync("Error", result.Data);
        }
    }
}
