namespace GameChat.Core.DTOs
{
    public class MessageNotificationDto
    {
        public MessageNotificationDto(int conversationId, int quantity)
        {
            ConversationId = conversationId;
            QuantityOfUnreadMessages = quantity;
        }

        public int ConversationId { get; private set; }
        public int QuantityOfUnreadMessages { get; private set; }
    }
}
