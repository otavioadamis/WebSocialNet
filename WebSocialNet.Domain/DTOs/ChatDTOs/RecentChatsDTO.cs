using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNet.Domain.DTOs.ChatDTOs
{
    public class RecentChatsDTO
    {
        public string ChatName { get; set; } = null!;
        public string LastMessage { get; set; } = null!;
    }
}
