﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocialNet.Domain.DTOs.ChatDTOs;
using WebSocialNet.Domain.Entities;

namespace WebSocialNet.Domain.Interfaces.IServices
{
    public interface IUserChatService
    {
        public IEnumerable<User>? SearchUsers(string keyword, string currentUserId);
        public ChatResponseDTO CreateChat(string currentUserId, string userId);
        public ChatResponseDTO GetChat(string currentUserId, string userId);
    }
}