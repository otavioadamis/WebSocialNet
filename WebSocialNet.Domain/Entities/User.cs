using System.ComponentModel.DataAnnotations;

namespace WebSocialNet.Domain.Entities
{
    public class User
    {
        [Key]
        public string Id { get; set; } = null!;
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public string? Bio { get; set; }

        public ICollection<Chat>? Chats { get; set; }
    }
}
