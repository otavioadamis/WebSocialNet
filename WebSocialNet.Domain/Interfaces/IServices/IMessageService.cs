using WebSocialNet.Domain.DTOs.MessageDTOs;
using WebSocialNet.Domain.Entities;

namespace WebSocialNet.Domain.Interfaces.IServices
{
    public interface IMessageService
    {
        public MessageDTO SendMessage(string toChatId, SendMessageDTO message, string senderName);
        public IEnumerable<MessageDTO> GetMessagesFromChat(string chatId);
    }
}
