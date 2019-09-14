using GameChat.Core.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GameChat.Web.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(MessageDto message)
        {
            await Clients.All.SendAsync("SendMessage", message);
        }
    }
}
