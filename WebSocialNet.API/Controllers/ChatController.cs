using Microsoft.AspNetCore.Mvc;
using WebSocialNet.API.Authorization;
using WebSocialNet.Domain.Entities;
using WebSocialNet.Domain.Interfaces.IServices;

namespace WebSocialNet.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [CustomAuthorize]
        [HttpGet("search")]
        public IActionResult SearchUsers(string keyword) 
        {
            var user = (User)HttpContext.Items["User"];
            var foundedUsers = _chatService.SearchUsers(keyword, user.Id);
            return Ok(foundedUsers);
        }

        [CustomAuthorize]
        [HttpPost("singlechat")]
        public IActionResult CreateChat(string receiverId) 
        {
            var user = (User)HttpContext.Items["User"];
            var createdChat = _chatService.CreateChat(user.Id, receiverId) ;
            return Ok(createdChat);
        }

        [CustomAuthorize]
        [HttpPost("groupchat")]
        public IActionResult CreateGroupChat(List<string> usersIds, string chatName)
        {
            var user = (User)HttpContext.Items["User"];
            var createdChat = _chatService.CreateGroupChat(user.Id, usersIds, chatName);
            return Ok(createdChat);
        }

        [CustomAuthorize]
        [HttpPut("adduser/{chatId}")]
        public IActionResult AddUserToGroupChat(string chatId, string userToAddId)
        {
            var groupChat = _chatService.AddToGroup(userToAddId, chatId);
            return Ok(groupChat);
        }

        [CustomAuthorize]
        [HttpPut("removeuser/{chatId}")]
        public IActionResult RemoveUserFromGroupChat(string chatId, string userToAddId)
        {
            var groupChat = _chatService.RemoveFromGroup(userToAddId, chatId);
            return Ok(groupChat);
        }

        [CustomAuthorize]
        [HttpGet()]
        public IActionResult GetChat(string chatId)
        {
            var user = (User)HttpContext.Items["User"];
            var findedChat = _chatService.GetChat(chatId, user.Id);
            return Ok(findedChat);
        }

        [CustomAuthorize]
        [HttpGet("recentchats")]
        public IActionResult GetRecentChats()
        {
            var user = (User)HttpContext.Items["User"];
            var recentChats = _chatService.GetRecentChats(user.Id);
            return Ok(recentChats);
        }

        [CustomAuthorize]
        [HttpPut("{chatId}")]
        public IActionResult RenameGroupChat(string chatId, string newChatName)
        {
            var newGroupChatName = _chatService.RenameGroupChat(chatId, newChatName);
            return Ok(newGroupChatName);
        }
    }
}
