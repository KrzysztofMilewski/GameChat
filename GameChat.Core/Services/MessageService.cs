using GameChat.Core.DTOs;
using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Interfaces.Services;
using GameChat.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameChat.Core.Services
{
    //TODO change name to conversation service, start building bussiness logic, including validation etc.
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SendMessage(MessageDto messageDto)
        {
            var message = new Message()
            {
                Contents = messageDto.Contents,
                ConversationId = messageDto.ConversationId,
                DateSent = DateTime.Now,
                SenderId = messageDto.SenderId
            };

            await _unitOfWork.MessageRepository.AddMessageAsync(message);
            await _unitOfWork.CompleteTransactionAsync();
        }

        public async Task<IEnumerable<MessageDto>> GetMessagesForConversation(int conversationId)
        {
            var messages = await _unitOfWork.MessageRepository.GetMessagesForConversation(conversationId);

            //TODO Replace with AutoMapper, change return types to ServiceResult

            var messagesDto = new List<MessageDto>();

            foreach (var message in messages)
                messagesDto.Add(new MessageDto() { Contents = message.Contents, DateSent = message.DateSent, SenderId = message.SenderId, ConversationId = message.ConversationId });

            return messagesDto;
        }
    }
}
