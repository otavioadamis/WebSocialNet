using WebSocialNet.Domain.Interfaces.IRepositories;
using WebSocialNet.Domain.Interfaces.IServices;

namespace WebSocialNet.Service
{
    public class UserFriendService
    {
        private readonly IUserRepo _userRepository;
        private readonly IAuthorizationService _authorizationService;

        public UserFriendService(IUserRepo userRepository, IAuthorizationService authorizationService)
        {
            _userRepository = userRepository;
            _authorizationService = authorizationService;
        }
    }
}
