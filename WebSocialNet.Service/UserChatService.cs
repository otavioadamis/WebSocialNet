using WebSocialNet.Domain.DTOs.ChatDTOs;
using WebSocialNet.Domain.Entities;
using WebSocialNet.Domain.Interfaces.IRepositories;
using WebSocialNet.Domain.Interfaces.IServices;

namespace WebSocialNet.Service
{
    public class UserChatService : IUserChatService
    {
        private readonly IUserRepo _userRepository;
        private readonly IChatRepo _chatRepository;

        public UserChatService(IUserRepo userRepository, IChatRepo chatRepository)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
        }

        public IEnumerable<User>? SearchUsers(string keyword, string currentUserId)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return Enumerable.Empty<User>();
            }
            
            //TODO Return user response models
            var foundUsers = _userRepository.SearchUsers(keyword, currentUserId);            
            return foundUsers;
        }

        public IEnumerable<ChatDTO> GetChat(string currentUserId, string userId)
        {
            var chatAndUser = _chatRepository.GetChatsWithUsersIds(currentUserId, userId);
                if(chatAndUser == null) { throw new ArgumentException("chat not found"); }
            
            return chatAndUser;
        }

        public ChatDTO CreateChat(string currentUserId, string userId)
        {
            var chatUser = _userRepository.GetById(userId);
            var currentUser = _userRepository.GetById(currentUserId);

            if (currentUser == null || chatUser == null)
            {
                throw new ArgumentException("Error finding users!");
            }

            var newChat = new Chat()
            {
                ChatName = chatUser.Name,
                UsersId = new List<string>{ userId, currentUserId }
            };
            
            _chatRepository.Add(newChat);
            _chatRepository.SaveChanges();

            var fullChat = _chatRepository.GetById(newChat.ChatId);

            var newChatDTO = new ChatDTO()
            {
                ChatName = chatUser.Name,
                UserEmail = chatUser.Email,
            };
            
            return newChatDTO;
        }

        public IEnumerable<RecentChatsDTO> GetRecentChats(string userId) 
        {
            var recentChats = _chatRepository.GetRecentChatsFromUserId(userId);
            return recentChats;
        }
    }
}
