using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNet.Domain.DTOs.MessageDTOs
{
    public class MessageDTO
    {
        public string Content { get; set; } = null!;
        public string SenderName { get; set; } = null!;
        public DateTime SendedAt { get; set; }
    }
}
