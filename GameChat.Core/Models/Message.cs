using System;
using System.Collections.Generic;

namespace GameChat.Core.Models
{
    public class Message
    {
        public int Id { get; set; }

        public Conversation Conversation { get; set; }
        public int ConversationId { get; set; }

        public User Sender { get; set; }
        public int SenderId { get; set; }

        public bool ReadByAllParticipants { get; set; }

        public string Contents { get; set; }
        public DateTime DateSent { get; set; }

        public ICollection<UnreadMessage> UsersThatHaventReadMessage { get; set; }
    }
}
