using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNet.Domain.Entities
{
    public class Post
    {
        [Key]
        public string Id { get; set; } = null!;
        public required string Category { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public byte[]? PostImage { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
