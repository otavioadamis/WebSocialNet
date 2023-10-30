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

        // TODO Reformular banco, criar tabela auxiliar many to many chat e users, reformular querys.
        public IEnumerable<ChatDTO> GetChatsWithUsersIds(string currentUserId, string userId)
        {
            var foundChat =  from chat in _dbContext.Chats
                             where chat.UsersId.Contains(userId) && chat.UsersId.Contains(currentUserId)
                             join chatUser in _dbContext.Users on userId equals chatUser.Id
                             select new ChatDTO
                             {
                                ChatName = chatUser.Name,
                                UserEmail = chatUser.Email,
                             };
            return foundChat;
        }

        // TODO adjust this when i have UserChat table (exclude UsersId list)
        public IEnumerable<RecentChatsDTO> GetRecentChatsFromUserId(string userId)
        {
            var recentChats = from chat in _dbContext.Chats
                              where chat.UsersId.Contains(userId)
                              join user in _dbContext.Users on chat.UsersId.First() equals user.Id
                              select new RecentChatsDTO
                              {
                                  LastMessage = chat.LatestMessage.Content,
                                  ChatName = user.Name,
                              };
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
