using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNet.Domain.Entities
{
    public class Friendship
    {
        [Key]
        public string FriendshipId { get; set; } = null!; // Primary key
        
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        
        public string FriendId { get; set; } = null!;
        public User Friend { get; set; } = null!;

    }
}
