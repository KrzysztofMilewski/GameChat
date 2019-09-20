using AutoMapper;
using GameChat.Core.DTOs;
using GameChat.Core.Helpers;
using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Interfaces.Services;
using GameChat.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public async Task<ServiceResult<MessageDto>> SendMessageAsync(MessageDto messageDto)
        {
            bool isUserParticipating =
                await _unitOfWork.
                ConversationRepository.
                IsUserParticipatingAsync(messageDto.ConversationId, messageDto.Sender.Id);

            if (!isUserParticipating)
                return new ServiceResult<MessageDto>(false, "Specified user is not a part of this conversation");

            if (string.IsNullOrWhiteSpace(messageDto.Contents))
                return new ServiceResult<MessageDto>(false, "Message cannot be empty");

            messageDto.DateSent = DateTime.Now;
            var messageEntity = _mapper.Map<Message>(messageDto);
            messageEntity.UsersThatHaventReadMessage = new Collection<UnreadMessage>();

            var conversationParticipants = await _unitOfWork.ConversationRepository.GetParticipantsAsync(messageDto.ConversationId);
            foreach (var participant in conversationParticipants)
            {
                if (participant.ParticipantId != messageDto.Sender.Id)
                    messageEntity.UsersThatHaventReadMessage.Add(new UnreadMessage() { ParticipantId = participant.ParticipantId });
            }

            await _unitOfWork.MessageRepository.AddMessageAsync(messageEntity);
            await _unitOfWork.CompleteTransactionAsync();
            messageDto.Id = messageEntity.Id;

            return new ServiceResult<MessageDto>(true, "Message has been sent");
        }

        public async Task<ServiceResult<IEnumerable<MessageDto>>> GetMessagesForConversationAsync(int conversationId, int requestingUserId)
        {
            bool isUserParticipating =
                await _unitOfWork.
                ConversationRepository.
                IsUserParticipatingAsync(conversationId, requestingUserId);

            if (!isUserParticipating)
                return new ServiceResult<IEnumerable<MessageDto>>(false, "User has no access to specified conversation");

            var messages = await _unitOfWork.MessageRepository.GetMessagesForConversationAsync(conversationId);
            _unitOfWork.MessageRepository.DeleteUnreadMarks(conversationId, requestingUserId);

            var messagesDto = _mapper.Map<IEnumerable<MessageDto>>(messages);

            await _unitOfWork.CompleteTransactionAsync();

            return new ServiceResult<IEnumerable<MessageDto>>(true, "Messages retrieved successfully", messagesDto);
        }

        public async Task<ServiceResult> ReadMessageAsync(int messageId, int readingUserId)
        {
            var unreadMessageMark = await _unitOfWork.MessageRepository.GetUnreadMarkAsync(messageId, readingUserId);

            if (unreadMessageMark == null)
                return new ServiceResult(false, "Cannot read that message");

            _unitOfWork.MessageRepository.DeleteUnreadMark(unreadMessageMark);
            await _unitOfWork.CompleteTransactionAsync();

            return new ServiceResult(true, "Message has beed read");
        }
    }
}
