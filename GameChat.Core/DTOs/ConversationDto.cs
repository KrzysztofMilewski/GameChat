using System.Collections.Generic;

namespace GameChat.Core.DTOs
{
    public class ConversationDto
    {
        public string Title { get; set; }
        public ICollection<UserDto> Participants { get; set; }
    }
}
