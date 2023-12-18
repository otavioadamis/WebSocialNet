using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocialNet.Domain.DTOs.ChatDTOs;
using WebSocialNet.Domain.Entities;

namespace WebSocialNet.Domain.Interfaces.IRepositories
{
    public interface IChatRepo
    {
        public void SaveChanges();
        public Chat Add(Chat thisChat);
        public List<Chat> Get();
        public IEnumerable<ChatDTO> GetChatsWithUsersIds(string chatId, string senderUserId);
        public IEnumerable<RecentChatsDTO> GetRecentChatsFromUserId(string userId);
        public Chat GetById(string _id);
        public void Delete(string _id);
    }
}
