using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNet.Domain.Interfaces.IServices
{
    public interface IChatHubService
    {
        Task ReceiveMessage(string message);
    }
}
