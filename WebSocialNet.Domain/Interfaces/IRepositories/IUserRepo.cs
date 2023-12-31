﻿using WebSocialNet.Domain.Entities;

namespace WebSocialNet.Domain.Interfaces.IRepositories
{
    public interface IUserRepo
    {
        User Add(User thisUser);
        List<User> Get();
        public List<User> SearchUsers(string keyword, string currentUserId);
        public IEnumerable<User> GetSenderAndUsersListByIds(string senderUserId, List<string> usersIds);
        User GetById(string _id);
        User GetByEmail(string _email);
        void SaveChanges();
        void Delete(string _id);
    }
}
