using Microsoft.EntityFrameworkCore;
using WebSocialNet.Dal.Data;
using WebSocialNet.Domain.Entities;
using WebSocialNet.Domain.Interfaces.IRepositories;

namespace WebSocialNet.Dal.Repositories
{
    public class ChatRepo : IChatRepo
    {
        //TODO AppDbContext and connect with POSTGRESQL
        private readonly AppDbContext _dbContext;
        public ChatRepo(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        // Save changes (?)
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        // Create
        public Chat Add(Chat thisChat)
        {
            _dbContext.Chats.Add(thisChat);
            return thisChat;
        }

        // Read
        public List<Chat> Get()
        {
            var allChats = _dbContext.Chats.ToList();
            return allChats;
        }

        public Chat FindChatsWithUserIds(string currentUserId, string userId)
        {
            var findedChat = _dbContext.Chats
                .Where(chat => !chat.IsGroupChat && chat.UsersId.Any(user => user.Equals(currentUserId)))
                .Where(chat => chat.UsersId.Any(user => user.Equals(userId)))
                .FirstOrDefault();

            return findedChat;
        }

        public Chat GetById(string _id)
        {
            var chat = _dbContext.Chats.FirstOrDefault(x => x.ChatId == _id);
            return chat;
        }

        // Delete
        public void Delete(string _id)
        {
            var chat = _dbContext.Chats.FirstOrDefault(x => x.ChatId == _id);
            _dbContext.Chats.Remove(chat);
        }
    }
}
