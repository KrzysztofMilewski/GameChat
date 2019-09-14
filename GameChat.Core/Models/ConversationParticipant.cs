namespace GameChat.Core.Models
{
    public class ConversationParticipant
    {
        public User Participant { get; set; }
        public int ParticipantId { get; set; }

        public Conversation Conversation { get; set; }
        public int ConversationId { get; set; }
    }
}
