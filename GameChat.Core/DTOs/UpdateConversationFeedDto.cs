using System;

namespace GameChat.Core.DTOs
{
    public class UpdateConversationFeedDto
    {
        public UpdateConversationFeedDto(int conversationId, DateTime lastMessageSent)
        {
            ConversationId = conversationId;
            LastMessageSent = lastMessageSent;
        }

        public int ConversationId { get; set; }
        public DateTime LastMessageSent { get; set; }
    }
}
