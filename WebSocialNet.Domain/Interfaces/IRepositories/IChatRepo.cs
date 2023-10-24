using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocialNet.Domain.Entities;

namespace WebSocialNet.Domain.Interfaces.IRepositories
{
    public interface IChatRepo
    {
        public void SaveChanges();
        public Chat Add(Chat thisChat);
        public List<Chat> Get();
        public Chat FindChatsWithUserIds(string currentUserId, string userId);
        public IEnumerable<Chat> GetAllChatsFromUserId(string userId);
        public Chat GetById(string _id);
        public void Delete(string _id);
    }
}
