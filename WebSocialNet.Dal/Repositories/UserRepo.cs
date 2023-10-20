using Microsoft.EntityFrameworkCore;
using WebSocialNet.Dal.Data;
using WebSocialNet.Domain.Entities;
using WebSocialNet.Domain.Interfaces.IRepositories;

namespace WebSocialNet.Dal.Repositories
{
    public class UserRepo : IUserRepo
    {
        //TODO AppDbContext and connect with POSTGRESQL
        private readonly AppDbContext _dbContext;
        public UserRepo(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        // Save changes (?)
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        // Create
        public User Add(User thisUser)
        {
            _dbContext.Users.Add(thisUser);
            return thisUser;
        }

        // Read
        public List<User> Get()
        {
            var allUsers = _dbContext.Users.ToList();
            return allUsers;
        }

        public List<User> SearchUsers(string keyword, string currentUserId)
        {
            return _dbContext.Users
                .Where(u => EF.Functions.ILike(u.Name, $"%{keyword}%") || EF.Functions.ILike(u.Email, $"%{keyword}%"))
                .Where(u => u.Id != currentUserId)
                .ToList();
        }

        public User GetById(string _id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == _id);
            return user;
        }
        public User GetByEmail(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == email);
            return user;
        }

        // Delete
        public void Delete(string _id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == _id);
            _dbContext.Users.Remove(user);
        }
    }
}
