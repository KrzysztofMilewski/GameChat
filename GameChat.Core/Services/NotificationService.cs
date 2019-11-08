using GameChat.Core.DTOs;
using GameChat.Core.Helpers;
using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameChat.Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<IEnumerable<SimpleMessageNotificationDto>>> GetUnreadMessagesNotifications(int userId)
        {
            var unreadMessages = await _unitOfWork.MessageRepository.GetUnreadMessagesAsync(userId);
            var groupedByConversation = unreadMessages.GroupBy(um => um.Message.ConversationId);

            var notifications = new List<SimpleMessageNotificationDto>();

            foreach (var grouped in groupedByConversation)
                notifications.Add(new SimpleMessageNotificationDto(grouped.Key, grouped.Count()));

            return new ServiceResult<IEnumerable<SimpleMessageNotificationDto>>(true, "Notifications retrieved successfully", notifications);
        }
    }
}
