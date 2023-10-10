using WebSocialNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNet.Domain.DTOs.UserDTOs
{
    public class UserResponseDTO
    {
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[]? ProfilePicture { get; set; }
        public string? Bio { get; set; }

        public UserResponseDTO CreateModel(User user)
        {
            var userModel = new UserResponseDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Bio = user.Bio,
                ProfilePicture = user.ProfilePicture,
            };
            return userModel;
        }
    }
}
