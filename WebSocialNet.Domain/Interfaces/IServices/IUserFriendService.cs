using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocialNet.Domain.DTOs.FriendshipDTOs;

namespace WebSocialNet.Domain.Interfaces.IServices
{
    public interface IUserFriendService 
    {
        public void CreateFriendship(string userId, string friendId);
        public IEnumerable<FriendshipDTO> GetAllFriendsFromUserId(string userId);
    }
    
}
