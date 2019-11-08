using System;

namespace GameChat.Core.DTOs
{
    public class ConversationFeedDto
    {
        public ConversationDto Conversation { get; set; }
        public int UnreadMessages { get; set; }
        public DateTime? LastMessageSent { get; set; }
    }
}
