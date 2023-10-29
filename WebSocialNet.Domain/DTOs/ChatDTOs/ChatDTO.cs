﻿using System;
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
        public string ChatName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
    }
}
