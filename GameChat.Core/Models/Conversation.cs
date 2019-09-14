using System.Collections.Generic;

namespace GameChat.Core.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<ConversationParticipant> Participants { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
