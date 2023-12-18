using System.Runtime.Intrinsics.X86;
using System;
using WebSocialNet.Domain.DTOs.ChatDTOs;
using WebSocialNet.Domain.Entities;
using WebSocialNet.Domain.Interfaces.IRepositories;
using WebSocialNet.Domain.Interfaces.IServices;

namespace WebSocialNet.Service
{
    public class ChatService : IChatService
    {
        private readonly IUserRepo _userRepository;
        private readonly IChatRepo _chatRepository;


        public ChatService(IUserRepo userRepository, IChatRepo chatRepository)
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

        public IEnumerable<ChatDTO> GetChat(string chatId, string senderUserId)
        {
            var chatAndUser = _chatRepository.GetChatsWithUsersIds(chatId, senderUserId);
                if(chatAndUser == null) { throw new ArgumentException("chat not found"); }
            
            return chatAndUser;
        }

        public ChatDTO CreateChat(string senderUserId, string receiverId)
        {
            var chatUser = _userRepository.GetById(receiverId);
            var currentUser = _userRepository.GetById(senderUserId);

            if (currentUser == null || chatUser == null)
            {
                throw new ArgumentException("Error finding users!");
            }

            var newChat = new Chat()
            {
                ChatName = "SingleChat",
                Users = new List<User>() { currentUser, chatUser },
            };
     
            _chatRepository.Add(newChat);
            _chatRepository.SaveChanges();

            var newChatDTO = new ChatDTO()
            {
                ChatName = chatUser.Name,
                UserEmail = chatUser.Email,
            };          
            return newChatDTO;
        }

        public GroupChatDTO CreateGroupChat(string senderUserId, List<string> receiversUsersIds, string chatName)
        {
            var groupChatUsersList = _userRepository.GetSenderAndUsersListByIds(senderUserId, receiversUsersIds);

            if (groupChatUsersList == null)
            {
                throw new ArgumentException("Error finding users!");
            }
            
            var newChat = new Chat()
            {
                ChatName = chatName,
                Users = groupChatUsersList.ToList(),
                IsGroupChat = true,
                UserAdminId = senderUserId,
            };

            _chatRepository.Add(newChat);
            _chatRepository.SaveChanges();

            var newGroupChatDTO = new GroupChatDTO()
            {
                ChatName = newChat.ChatName,
            };
            return newGroupChatDTO;
        }

        public GroupChatDTO AddToGroup(string loggedInUserId, string userId, string groupChatId)
        {
            var chat = _chatRepository.GetById(groupChatId);
            if (chat == null) { throw new ArgumentException("Chat not found"); }

            var userToAdd = _userRepository.GetById(userId);
            if (userToAdd == null) { throw new ArgumentException("User Not Found"); }

            if (loggedInUserId == chat.UserAdminId && !chat.Users.Contains(userToAdd))
            {

                chat.Users.Add(userToAdd);
                _chatRepository.SaveChanges();

                var groupChatDTO = new GroupChatDTO()
                {
                    ChatName = chat.ChatName,
                };

                return groupChatDTO;
            }
            else { throw new ArgumentException("Not allowed!"); }
        }

        public GroupChatDTO RemoveFromGroup(string loggedInUserId, string userId, string groupChatId)
        {
            var chat = _chatRepository.GetById(groupChatId);
            if (chat == null) { throw new ArgumentException("Chat not found"); }

            if (loggedInUserId == chat.UserAdminId)
            {
                var userToRemove = _userRepository.GetById(userId);
                if (!chat.Users.Contains(userToRemove)) { throw new ArgumentException("User not founded in chat"); }

                chat.Users.Remove(userToRemove);
                _chatRepository.SaveChanges();

                var groupChatDTO = new GroupChatDTO()
                {
                    ChatName = chat.ChatName,
                };

                return groupChatDTO;
            }
            else { throw new ArgumentException("Not allowed!"); }
        }

        public IEnumerable<RecentChatsDTO> GetRecentChats(string userId) 
        {
            var recentChats = _chatRepository.GetRecentChatsFromUserId(userId);
            return recentChats;
        }

        public GroupChatDTO RenameGroupChat(string loggedInUserId, string chatId, string newGroupName)
        {
            var chat = _chatRepository.GetById(chatId);
            
            if(chat.IsGroupChat && loggedInUserId == chat.UserAdminId) 
            {
                chat.ChatName = newGroupName;
                _chatRepository.SaveChanges();

                var newGroupChatDTO = new GroupChatDTO()
                {
                    ChatName = chat.ChatName,
                };

                return newGroupChatDTO;
            }
            throw new ArgumentException("Cannot change the name of this chat!");
        }
    }
}
