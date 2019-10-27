using GameChat.Core.Interfaces.Services;
using GameChat.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GameChat.Web.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private readonly INotificationService _notificationService;

        public NotificationHub(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task GetUnreadMessagesNotifications()
        {
            var currentUserId = Context.User.GetUserId();
            var result = await _notificationService.GetUnreadMessagesNotifications(currentUserId);
            var notifications = result.Data;

            await Clients.Caller.SendAsync("InitialNotificationsLoad", notifications);
        }
    }
}
