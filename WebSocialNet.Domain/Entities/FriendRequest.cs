using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNet.Domain.Entities
{
    public class FriendRequest
    {
        public string FriendRequestId { get; set; } = null!; // Primary key

        public string SenderId { get; set; } = null!;
        public User Sender { get; set; } = null!;

        public string ReceiverId { get; set; } = null!;
        public User Receiver { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime RequestDate { get; set; }
    }
}
