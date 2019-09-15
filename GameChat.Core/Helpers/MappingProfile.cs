using AutoMapper;
using GameChat.Core.DTOs;
using GameChat.Core.Models;

namespace GameChat.Core.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Message, MessageDto>();
            CreateMap<MessageDto, Message>();

            CreateMap<User, UserDto>();
        }
    }
}
