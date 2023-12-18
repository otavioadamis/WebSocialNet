using WebSocialNet.Domain.DTOs.FriendshipDTOs;
using WebSocialNet.Domain.Entities;
using WebSocialNet.Domain.Interfaces.IRepositories;
using WebSocialNet.Domain.Interfaces.IServices;

namespace WebSocialNet.Service
{
    public class UserFriendService : IUserFriendService
    {
        private readonly IUserRepo _userRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IFriendshipRepo _friendshipRepo;

        public UserFriendService(IUserRepo userRepository, IAuthorizationService authorizationService, IFriendshipRepo friendshipRepo)
        {
            _userRepository = userRepository;
            _authorizationService = authorizationService;
            _friendshipRepo = friendshipRepo;
        }
        
        public void CreateFriendship(string userId, string friendId)
        {
            //todo -> check if both users exists
            //todo -> check if users are not already friends
            {
                var friendship = new Friendship
                {
                    UserId = userId,
                    FriendId = friendId,
                    FriendsAt = DateTime.UtcNow
                };

                _friendshipRepo.Add(friendship);
                _friendshipRepo.SaveChanges();
            }
        }

        public IEnumerable<FriendshipDTO> GetAllFriendsFromUserId(string userId)
        {
            var allFriends = _friendshipRepo.GetAllFriendsFromUserId(userId);
            return allFriends;
        }
    }
}
