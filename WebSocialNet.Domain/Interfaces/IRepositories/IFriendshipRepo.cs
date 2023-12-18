using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocialNet.Domain.DTOs.FriendshipDTOs;
using WebSocialNet.Domain.Entities;

namespace WebSocialNet.Domain.Interfaces.IRepositories
{
    public interface IFriendshipRepo
    {
        public void SaveChanges();
        public Friendship Add(Friendship thisFriendship);
        public IEnumerable<FriendshipDTO> GetAllFriendsFromUserId(string userId);
        public void Delete(string _id);
    }
}
