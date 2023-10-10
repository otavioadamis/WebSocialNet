using WebSocialNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNet.Domain.Interfaces.IServices
{
    public interface IAuthorizationService
    {
        public string CreateToken(User thisUser);
        public string? ValidateJwtToken(string token);
    }
}
