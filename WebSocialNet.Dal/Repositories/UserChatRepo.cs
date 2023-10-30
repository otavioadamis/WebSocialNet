using System.Runtime.Intrinsics.X86;
using System;
using WebSocialNet.Dal.Data;
using WebSocialNet.Domain.Entities;
using WebSocialNet.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace WebSocialNet.Dal.Repositories
{
    public class UserChatRepo : IUserChatRepo
    {
        private readonly AppDbContext _dbContext;

        public UserChatRepo(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        // Save changes (?)
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        // Create
        public UserChat Add(UserChat userChat)
        {
            _dbContext.UsersChats.Add(userChat);
            return userChat;
        }

        // Read
        public List<UserChat> Get()
        {
            var allUsersChats = _dbContext.UsersChats.ToList();
            return allUsersChats;
        }

        public UserChat GetById(string _id)
        {
            var userChat = _dbContext.UsersChats.FirstOrDefault(x => x.Id == _id);
            return userChat;
        }

        // Delete
        public void Delete(string _id)
        {
            var userChat = _dbContext.UsersChats.FirstOrDefault(x => x.Id == _id);
            _dbContext.UsersChats.Remove(userChat);
        }
    }
}
