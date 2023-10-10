using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNet.Domain.DTOs.UserDTOs
{
    public class LoginResponseModel
    {
        public required string Token { get; set; }
        public required UserResponseDTO User { get; set; }
    }
}
