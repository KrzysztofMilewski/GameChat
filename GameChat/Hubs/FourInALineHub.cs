using GameChat.Core.DTOs;
using GameChat.Games.GameEngines;
using GameChat.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameChat.Web.Hubs
{
    [Authorize]
    public class FourInALineHub : Hub
    {
        private readonly IHubContext<NotificationHub> _notificationHub;
        private static readonly Dictionary<Guid, FourInALineEngine> _activeGames = new Dictionary<Guid, FourInALineEngine>();

        public FourInALineHub(IHubContext<NotificationHub> notificationHub)
        {
            _notificationHub = notificationHub;
        }

        public async Task ChallengePlayer(int playerToChallengeId)
        {
            int invitingPlayerId = Context.User.GetUserId();
            var challengeToken = new GameChallengeDto()
            {
                GameId = Guid.NewGuid(),
                InvitingPlayerId = invitingPlayerId,
                ChallengedPlayerId = playerToChallengeId,
                GameName = "Four in a line",
                ExpirationTime = DateTime.Now + TimeSpan.FromSeconds(60)
            };

            _activeGames.Add(challengeToken.GameId, new FourInALineEngine(challengeToken.InvitingPlayerId, challengeToken.ChallengedPlayerId));

            await Groups.AddToGroupAsync(Context.ConnectionId, "GameId: " + challengeToken.GameId.ToString());
            await _notificationHub.Clients.User(playerToChallengeId.ToString()).SendAsync("GameChallenge", challengeToken);
        }
        public async Task AcceptChallenge(Guid gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "GameId: " + gameId.ToString());
            await Clients.Group("GameId: " + gameId.ToString()).SendAsync("Accepted");
        }
    }
}
