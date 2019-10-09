using AutoMapper;
using GameChat.Core.DTOs;
using GameChat.Core.Models;

namespace GameChat.Core.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Domain to DTO

            CreateMap<Message, MessageDto>();
            CreateMap<User, UserDto>();
            CreateMap<Conversation, ConversationDto>();

            #endregion


            #region DTO to Domain

            CreateMap<MessageDto, Message>().
                ForMember(m => m.SenderId, opt => opt.MapFrom(dto => dto.Sender.Id)).
                ForMember(m => m.Sender, opt => opt.Ignore());

            CreateMap<UserDto, User>();

            #endregion
        }
    }
}
