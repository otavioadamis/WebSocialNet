using System.ComponentModel.DataAnnotations;
using WebSocialNet.Domain.DTOs.UserDTOs;

namespace WebSocialNet.Domain.Entities
{
    public class Message
    {
        [Key]
        public string Id { get; set; } = null!;
        public string Sender { get; set; } = null!;
        public string Content { get; set; } = null!;
        
        public Chat Chat { get; set; } = null!; // Navigation property
        public string ChatId { get; set; } = null!;
        public DateTime SendedAt { get; set; }
    }
}
