using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocialNet.Domain.Entities;

namespace WebSocialNet.Domain.Interfaces.IRepositories
{
    public interface IUserChatRepo
    {
        public void SaveChanges();
        public UserChat Add(UserChat userChat);
        public List<UserChat> Get();
        public UserChat GetById(string _id);
        public void Delete(string _id);
    }
}
