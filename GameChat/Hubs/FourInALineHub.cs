using GameChat.Core.DTOs;
using GameChat.Games.FourInALine;
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
            var initialBoardState = _activeGames[gameId].GetBoardState();
            await Clients.Group("GameId: " + gameId.ToString()).SendAsync("Accepted", gameId, initialBoardState);
        }

        public async Task MakeMove(Guid gameId, int xCoordinate)
        {
            var currentUserId = Context.User.GetUserId();
            var gameInstance = _activeGames[gameId];

            if (gameInstance.PlayerWhoHasTurn != currentUserId)
                return;

            gameInstance.PlaceDisc(xCoordinate, currentUserId);

            var boardState = gameInstance.GetBoardState();

            await Clients.Group("GameId: " + gameId.ToString()).SendAsync("DiscPlaced", boardState);

            if (gameInstance.GameEnded)
                await Clients.Group("GameId: " + gameId.ToString()).SendAsync("AnnounceWinner", gameInstance.WinnerId);
        }
    }
}
