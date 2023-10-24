using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocialNet.Domain.DTOs.UserDTOs;
using WebSocialNet.Domain.Entities;

namespace WebSocialNet.Domain.DTOs.ChatDTOs
{
    public class ChatDTO
    {
        public string ChatId { get; set; } = null!;
        public string ChatName { get; set; } = null!;
        public Message? latestMessage { get; set; }
        public string? UserAdminId { get; set; }
        public bool IsGroupChat { get; set; }
        public List<UserResponseDTO>? Users { get; set; }

        public ChatDTO CreateModel(Chat chat)
        {
            var chatModel = new ChatDTO()
            {
                ChatId = chat.ChatId,
                ChatName = chat.ChatName,
                latestMessage = chat.LatestMessage,
                UserAdminId = chat.UserAdminId,
                IsGroupChat = chat.IsGroupChat
            };
            return chatModel;
        }
    }
}
