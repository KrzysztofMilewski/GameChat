using System;

namespace GameChat.Core.DTOs
{
    public class MessageDto
    {
        public int ConversationId { get; set; }

        public int SenderId { get; set; }

        public string Contents { get; set; }
        public DateTime DateSent { get; set; }
    }
}
