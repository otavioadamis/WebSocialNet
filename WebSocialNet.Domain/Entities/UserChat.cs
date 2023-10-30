using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNet.Domain.Entities
{
    public class UserChat
    {
        public string Id { get; set; } = null!;

        public string ChatId { get; set; } = null!;
        public Chat Chat { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public DateTime EnrollmentDate { get; set; }
        public int? CompletedLessons { get; set; }
    }
}
