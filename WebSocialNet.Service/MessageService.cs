using WebSocialNet.Domain.DTOs.MessageDTOs;
using WebSocialNet.Domain.Entities;
using WebSocialNet.Domain.Interfaces.IRepositories;
using WebSocialNet.Domain.Interfaces.IServices;

namespace WebSocialNet.Service
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepo _messageRepo;
        private readonly IChatRepo _chatRepo;


        public MessageService(IMessageRepo messageRepo, IChatRepo chatRepo)
        {
            _messageRepo = messageRepo;
            _chatRepo = chatRepo;
        }

        public MessageDTO SendMessage(string toChatId, SendMessageDTO message, string senderName)
        {
            var newMessage = new Message()
            {
                ChatId = toChatId,
                Content = message.Content,
                Sender = message.SenderId,
                SendedAt = DateTime.UtcNow,
            };

            _messageRepo.Add(newMessage);
            _messageRepo.SaveChanges();

            var chat = _chatRepo.GetById(toChatId);
            chat.UpdatedAt = newMessage.SendedAt;
            _chatRepo.SaveChanges();

            var newMessageDTO = new MessageDTO()
            {
                Content = newMessage.Content,
                SenderName = newMessage.Sender,
                SendedAt = newMessage.SendedAt,
            };

            return newMessageDTO;
        }

        //todo adjust this
        public IEnumerable<MessageDTO> GetMessagesFromChat(string chatId) 
        {
            var allMessages = _messageRepo.GetMessagesFromChatId(chatId);
            return allMessages;
        }
    }
}
