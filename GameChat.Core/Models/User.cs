using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GameChat.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<Conversation> Conversations { get; set; }

        public User()
        {
            Conversations = new Collection<Conversation>();
        }
    }
}
