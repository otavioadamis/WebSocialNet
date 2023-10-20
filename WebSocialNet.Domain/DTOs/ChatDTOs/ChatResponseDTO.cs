using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocialNet.Domain.DTOs.UserDTOs;

namespace WebSocialNet.Domain.DTOs.ChatDTOs
{
    public class ChatResponseDTO
    {
        public SingleChatDTO Chat { get; set; } = null!;
        public List<UserResponseDTO> Users { get; set; } = null!;
    }
}
