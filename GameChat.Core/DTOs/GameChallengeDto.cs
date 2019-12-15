using System;

namespace GameChat.Core.DTOs
{
    public class GameChallengeDto
    {
        public Guid GameId { get; set; }
        public string GameName { get; set; }
        public int InvitingPlayerId { get; set; }
        public int ChallengedPlayerId { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
