using GameChat.Core.DTOs;
using GameChat.Core.Interfaces.Services;
using GameChat.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GameChat.Web.Hubs
{
    [Authorize]
    public class MessageHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IConversationService _conversationService;
        private readonly IHubContext<NotificationHub> _notificationHub;
        private readonly INotificationService _notificationService;

        public MessageHub(
            IMessageService messageService,
            IConversationService conversationService,
            IHubContext<NotificationHub> notificationHub,
            INotificationService notificationService)
        {
            _messageService = messageService;
            _conversationService = conversationService;
            _notificationHub = notificationHub;
            _notificationService = notificationService;
        }

        public async Task JoinConversation(int conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
        }

        public async Task ReceiveMessageFromClient(MessageDto message)
        {
            var currentUserId = Context.User.GetUserId();
            if (message.Sender.Id != currentUserId)
                await Clients.Caller.SendAsync("ConversationError", "Unauthorized access to conversation");

            var result = await _messageService.SendMessageAsync(message);

            if (!result.Success)
                await Clients.Caller.SendAsync("ConversationError", result.Message);
            else
            {
                await Clients.Group(message.ConversationId.ToString()).SendAsync("SendMessage", message);
                var allParticipants = await _conversationService.GetParticipants(message.ConversationId, currentUserId);

                foreach (var user in allParticipants.Data)
                    await _notificationHub.Clients.User(user.Id.ToString()).SendAsync("MessageNotification", message.ConversationId);
            }
        }
    }
}
