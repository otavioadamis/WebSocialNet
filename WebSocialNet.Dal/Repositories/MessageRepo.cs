using WebSocialNet.Dal.Data;
using WebSocialNet.Domain.DTOs.MessageDTOs;
using WebSocialNet.Domain.Entities;
using WebSocialNet.Domain.Interfaces.IRepositories;

namespace WebSocialNet.Dal.Repositories
{
    public class MessageRepo : IMessageRepo
    {
        private readonly AppDbContext _dbContext;
        public MessageRepo(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        // Save changes (?)
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        // Create
        public Message Add(Message thisMessage)
        {
            _dbContext.Messages.Add(thisMessage);
            return thisMessage;
        }

        // Read
        public IEnumerable<MessageDTO> GetMessagesFromChatId(string chatId)
        {
            var allMessages = from m in _dbContext.Messages
                              where m.ChatId == chatId
                              join senderUser in _dbContext.Users on m.Sender equals senderUser.Id
                              orderby m.SendedAt descending
                              select new MessageDTO
                              {
                                  Content = m.Content,
                                  SenderName = senderUser.Name,
                              };
            return allMessages.ToList();
        }

        public Message GetById(string _id)
        {
            var message = _dbContext.Messages.FirstOrDefault(x => x.Id == _id);
            return message;
        }

        // Delete
        public void Delete(string _id)
        {
            var message = _dbContext.Messages.FirstOrDefault(x => x.Id == _id);
            _dbContext.Messages.Remove(message);
        } 
    }
}
