using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNet.Domain.Entities
{
    public class Comment
    {
        [Key]
        public string Id { get; set; } = null!;
        public required string Text { get; set; }
        public byte[]? CommentImage { get; set; }
        public DateTime PostedAt { get; set; }
    }
}
