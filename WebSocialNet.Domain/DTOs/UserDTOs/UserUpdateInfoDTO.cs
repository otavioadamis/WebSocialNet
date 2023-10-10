using WebSocialNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNet.Domain.DTOs.UserDTOs
{
    public class UserUpdateInfoDTO
    {
        public string NewEmail { get; set; } = null!;
        public string NewName { get; set; } = null!;
        public byte[]? NewProfilePicture { get; set; }
        public string? NewBio { get; set; }

        public void UpdateFields(User user)
        {
            user.Name = NewName;
            user.Email = NewEmail;
            user.ProfilePicture = NewProfilePicture;
            user.Bio = NewBio;
        }
    }
}
