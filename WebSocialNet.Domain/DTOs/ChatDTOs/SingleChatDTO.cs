using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocialNet.Domain.DTOs.UserDTOs;
using WebSocialNet.Domain.Entities;

namespace WebSocialNet.Domain.DTOs.ChatDTOs
{
    public class SingleChatDTO
    {
        public string ChatId { get; set; } = null!;
        public string ChatName { get; set; } = null!;
        public Message? latestMessage { get; set; }

        public SingleChatDTO CreateModel(Chat chat)
        {
            var chatModel = new SingleChatDTO()
            {
                ChatId = chat.ChatId,
                ChatName = chat.ChatName,
                latestMessage = chat.LatestMessage
            };
            return chatModel;
        }
    }
}
