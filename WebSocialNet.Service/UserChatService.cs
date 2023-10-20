using WebSocialNet.Domain.DTOs.ChatDTOs;
using WebSocialNet.Domain.DTOs.UserDTOs;
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

        public ChatResponseDTO GetChat(string currentUserId, string userId)
        {
            var chat = _chatRepository.FindChatsWithUserIds(currentUserId, userId);

            if (chat != null)
            {
                var chatUser = _userRepository.GetById(userId);
                var currentUser = _userRepository.GetById(currentUserId);

                if (currentUser == null || chatUser == null) 
                {
                    throw new ArgumentException("Error finding users!");
                }

                var chatModel = new SingleChatDTO();
                chatModel.CreateModel(chat);

                var chatUserModel = new UserResponseDTO();
                chatUserModel = chatUserModel.CreateModel(chatUser);

                var currentUserModel = new UserResponseDTO();
                currentUserModel = currentUserModel.CreateModel(currentUser);

                var response = new ChatResponseDTO()
                {
                    Chat = chatModel,
                    Users = { chatUserModel, currentUserModel }
                };
            }
            throw new ArgumentException("Chat not found!");
        }

        public ChatResponseDTO CreateChat(string currentUserId, string userId)
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
                UsersId = { userId, currentUserId },
            };
            
            _chatRepository.Add(newChat);
            _chatRepository.SaveChanges();

            var fullChat = _chatRepository.GetById(newChat.ChatId);

            var createdChatModel = new SingleChatDTO();
            createdChatModel.CreateModel(fullChat);
            
            var chatUserModel = new UserResponseDTO();
            chatUserModel = chatUserModel.CreateModel(chatUser);

            var currentUserModel = new UserResponseDTO();
            currentUserModel = currentUserModel.CreateModel(currentUser);

            var res = new ChatResponseDTO()
            {
                Chat = createdChatModel,
                Users = { chatUserModel, currentUserModel }
            };
            return res;
        }
    }
}
