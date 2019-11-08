using AutoMapper;
using GameChat.Core.DTOs;
using GameChat.Core.Helpers;
using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Interfaces.Services;
using GameChat.Core.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GameChat.Core.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConversationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResult<int>> CreateNewConversation(ConversationDto conversationDto)
        {
            if (string.IsNullOrWhiteSpace(conversationDto.Title))
                return new ServiceResult<int>(false, "Conversation title cannot be empty. Please specify the title");

            if (conversationDto.Participants.Count < 2)
                return new ServiceResult<int>(false, "Conversation must have at least 2 participants");

            var participatingUsers = conversationDto.Participants.Select(u => new ConversationParticipant() { ParticipantId = u.Id }).ToList();

            var conversation = new Conversation()
            {
                Title = conversationDto.Title,
                Participants = new Collection<ConversationParticipant>(participatingUsers)
            };

            await _unitOfWork.ConversationRepository.CreateNewConversationAsync(conversation);
            await _unitOfWork.CompleteTransactionAsync();

            int id = conversation.Id;

            return new ServiceResult<int>(true, "Conversation created successfully", id);
        }

        public async Task<ServiceResult<IEnumerable<ConversationFeedDto>>> GetConversationsForUser(int userId)
        {
            var user = await _unitOfWork.UserRepository.FindByIdAsync(userId);

            if (user == null)
                return new ServiceResult<IEnumerable<ConversationFeedDto>>(false, "Specified user does not exist");

            var conversations = await _unitOfWork.ConversationRepository.GetConversationsForUserAsync(userId);
            var unreadMessages = await _unitOfWork.MessageRepository.GetUnreadMessagesAsync(userId);
            var conversationsDto = _mapper.Map<IEnumerable<ConversationDto>>(conversations);

            foreach (var conversation in conversationsDto)
            {
                var participants = await _unitOfWork.ConversationRepository.GetParticipantsAsUsersAsync(conversation.Id);
                var participantsDto = _mapper.Map<IEnumerable<UserDto>>(participants).ToList();
                conversation.Participants = new Collection<UserDto>(participantsDto);
            }

            var conversationsFeed = conversationsDto.GroupJoin(
                unreadMessages,
                c => c.Id,
                um => um.Message.Id,
                (c, ums) => new ConversationFeedDto()
                {
                    Conversation = c,
                    UnreadMessages = ums.Count(),
                    LastMessageSent = ums.FirstOrDefault()?.Message.DateSent
                });

            return new ServiceResult<IEnumerable<ConversationFeedDto>>(true, "Conversations retrieved successfully", conversationsFeed);
        }

        public async Task<ServiceResult<ConversationDto>> GetConversation(int conversationId, int requestingUserId)
        {
            var isParticipating = await _unitOfWork.ConversationRepository.IsUserParticipatingAsync(conversationId, requestingUserId);

            if (!isParticipating)
                return new ServiceResult<ConversationDto>(false, "This user is not a part of this conversation");


            //TODO fix this (possibly create a stack overflow post for that)
            var conversation = await _unitOfWork.ConversationRepository.GetConversationAsync(conversationId);
            conversation.Participants = null;
            var participants = await _unitOfWork.ConversationRepository.GetParticipantsAsUsersAsync(conversationId);

            var conversationDto = _mapper.Map<ConversationDto>(conversation);
            var participantsDto = _mapper.Map<IEnumerable<UserDto>>(participants).ToList();
            conversationDto.Participants = new Collection<UserDto>(participantsDto);

            return new ServiceResult<ConversationDto>(true, "Conversation info successfully retrieved", conversationDto);
        }

        public async Task<ServiceResult<IEnumerable<UserDto>>> GetParticipants(int conversationId, int requestingUserId)
        {
            bool isParticipating = await _unitOfWork.ConversationRepository.IsUserParticipatingAsync(conversationId, requestingUserId);
            if (!isParticipating)
                return new ServiceResult<IEnumerable<UserDto>>(false, "This user is not a part of this conversation");

            var participants = await _unitOfWork.ConversationRepository.GetParticipantsAsUsersAsync(conversationId);
            var participantsDto = _mapper.Map<IEnumerable<UserDto>>(participants);

            return new ServiceResult<IEnumerable<UserDto>>(true, "Users retrieved successfully", participantsDto);
        }
    }
}
