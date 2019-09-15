using AutoMapper;
using GameChat.Core.DTOs;
using GameChat.Core.Helpers;
using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Interfaces.Services;
using GameChat.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameChat.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResult> SendMessage(MessageDto messageDto)
        {
            bool isUserParticipating =
                await _unitOfWork.
                ConversationRepository.
                IsUserParticipatingAsync(messageDto.ConversationId, messageDto.SenderId);

            if (!isUserParticipating)
                return new ServiceResult(false, "Specified user is not a part of this conversation");

            if (string.IsNullOrWhiteSpace(messageDto.Contents))
                return new ServiceResult(false, "Message cannot be empty");

            messageDto.DateSent = DateTime.Now;
            await _unitOfWork.MessageRepository.AddMessageAsync(_mapper.Map<Message>(messageDto));
            await _unitOfWork.CompleteTransactionAsync();

            return new ServiceResult(true, "Message has been sent");
        }

        public async Task<ServiceResult<IEnumerable<MessageDto>>> GetMessagesForConversation(int conversationId, int requestingUserId)
        {
            bool isUserParticipating =
                await _unitOfWork.
                ConversationRepository.
                IsUserParticipatingAsync(conversationId, requestingUserId);

            if (!isUserParticipating)
                return new ServiceResult<IEnumerable<MessageDto>>(false, "User has no access to specified conversation");

            var messages = await _unitOfWork.MessageRepository.GetMessagesForConversationAsync(conversationId);
            var messagesDto = _mapper.Map<IEnumerable<MessageDto>>(messages);

            return new ServiceResult<IEnumerable<MessageDto>>(true, "Messages retrieved successfully", messagesDto);
        }
    }
}
