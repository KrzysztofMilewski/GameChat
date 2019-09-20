namespace GameChat.Core.Models
{
    public class UnreadMessage
    {
        public Message Message { get; set; }
        public int MessageId { get; set; }

        public User Participant { get; set; }
        public int ParticipantId { get; set; }
    }
}
