using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNet.Domain.DTOs.FriendshipDTOs
{
    public class FriendshipDTO
    {
        public string FriendName { get; set; } = null!;
        public string FriendEmail { get; set; } = null!;
        public DateTime FriendsAt { get; set; }

    }
}
