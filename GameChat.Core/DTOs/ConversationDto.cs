using System.Collections.Generic;

namespace GameChat.Core.DTOs
{
    public class ConversationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<UserDto> Participants { get; set; }
    }
}
