using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GameChat.Core.Models
{
    public class Conversation
    {
        public int Id { get; set; }

        public User Participant1 { get; set; }
        public int Participant1Id { get; set; }

        public User Participant2 { get; set; }
        public int Participant2Id { get; set; }

        public ICollection<Message> Messages { get; set; }

        public Conversation()
        {
            Messages = new Collection<Message>();
        }
    }
}
