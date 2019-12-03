using GameChat.Core.DTOs;
using GameChat.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace GameChat.Web.Hubs
{
    [Authorize]
    public class FourInALineHub : Hub
    {
        private readonly IHubContext<NotificationHub> _notificationHub;

        public FourInALineHub(IHubContext<NotificationHub> notificationHub)
        {
            _notificationHub = notificationHub;
        }

        public async Task ChallengePlayer(int playerToChallengeId)
        {
            int invitingPlayer = Context.User.GetUserId();
            var challengeToken = new GameChallengeDto()
            {
                InvitingPlayerId = invitingPlayer,
                ChallengedPlayerId = playerToChallengeId,
                GameName = "Four in a line",
                ExpirationTime = DateTime.Now + TimeSpan.FromSeconds(60)
            };

            await _notificationHub.Clients.User(playerToChallengeId.ToString()).SendAsync("GameChallenge", challengeToken);
        }
    }
}
