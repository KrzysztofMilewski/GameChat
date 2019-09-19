using System;

namespace GameChat.Core.DTOs
{
    public class MessageDto
    {
        public int ConversationId { get; set; }

        public UserDto Sender { get; set; }

        public string Contents { get; set; }

        public DateTime DateSent { get; set; }
    }
}
