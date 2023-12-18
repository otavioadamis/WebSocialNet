using Microsoft.EntityFrameworkCore;
using WebSocialNet.Dal.Data;
using WebSocialNet.Domain.DTOs.ChatDTOs;
using WebSocialNet.Domain.Entities;
using WebSocialNet.Domain.Interfaces.IRepositories;

namespace WebSocialNet.Dal.Repositories
{
    public class ChatRepo : IChatRepo
    {
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

        public IEnumerable<ChatDTO> GetChatsWithUsersIds(string chatId, string senderUserId)
        {
            var foundedChat = (
                from chat in _dbContext.Chats
                where chat.ChatId == chatId
                join userchat in _dbContext.UsersChats on chat.ChatId equals userchat.ChatId
                join receiver in _dbContext.Users on userchat.UserId equals receiver.Id
                where receiver.Id != senderUserId
                select new ChatDTO
                {
                    ChatName = receiver.Name,
                    UserEmail = receiver.Email
                });

            return foundedChat;
        }

        public IEnumerable<RecentChatsDTO> GetRecentChatsFromUserId(string userId)
        {
            var recentChats = (from usersChat in _dbContext.UsersChats
                              where usersChat.UserId == userId
                              join chat in _dbContext.Chats
                                  on usersChat.ChatId equals chat.ChatId
                              join otherUsersChat in _dbContext.UsersChats
                                  on chat.ChatId equals otherUsersChat.ChatId
                              where otherUsersChat.UserId != userId // Exclude the logged-in user
                              join otherUser in _dbContext.Users
                                  on otherUsersChat.UserId equals otherUser.Id
                              orderby chat.UpdatedAt descending
                              select new RecentChatsDTO
                              {
                                  LastMessage = chat.LatestMessage.Content,
                                  ChatName = chat.IsGroupChat ? chat.ChatName : otherUser.Name,
                              }).Distinct().ToList();
            return recentChats;
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
