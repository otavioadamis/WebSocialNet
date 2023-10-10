﻿using System;
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

        [ForeignKey("UserId")]
        public string UserId { get; set; } = null!;

        [ForeignKey("FriendId")]
        public string FriendId { get; set; } = null!;
    }
}
