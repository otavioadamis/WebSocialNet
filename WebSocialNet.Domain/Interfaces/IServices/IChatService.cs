using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocialNet.Domain.DTOs.ChatDTOs;
using WebSocialNet.Domain.Entities;

namespace WebSocialNet.Domain.Interfaces.IServices
{
    public interface IChatService
    {
        public IEnumerable<User>? SearchUsers(string keyword, string currentUserId);
        public ChatDTO CreateChat(string currentUserId, string userId);
        public GroupChatDTO CreateGroupChat(string senderUserId, List<string> receiversUsersIds, string chatName);
        public IEnumerable<ChatDTO> GetChat(string chatId, string senderUserId);
        public IEnumerable<RecentChatsDTO> GetRecentChats(string userId);
        public GroupChatDTO RenameGroupChat(string loggedInUserId, string chatId, string newGroupName);
        public GroupChatDTO AddToGroup(string loggedInUserId, string userId, string groupChatId);
        public GroupChatDTO RemoveFromGroup(string loggedInUserId, string userId, string groupChatId);
    }
}
