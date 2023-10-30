using WebSocialNet.Domain.DTOs.UserDTOs;

namespace WebSocialNet.Domain.Entities
{
    public class Chat
    {
        public string ChatId { get; set; } = null!;
        public string ChatName { get; set; } = null!;
        public bool IsGroupChat { get; set; } = false;
        public string? UserAdminId { get; set; } 
        public string? LatestMessageId { get; set; }
        
        public Message? LatestMessage { get; set; } // Navigation property
        public ICollection<User> Users { get; set; } = null!;
    }
}
