using GameChat.Core.DTOs;
using GameChat.Core.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameChat.Core.Interfaces.Services
{
    public interface INotificationService
    {
        Task<ServiceResult<IEnumerable<SimpleMessageNotificationDto>>> GetUnreadMessagesNotifications(int userId);
    }
}
