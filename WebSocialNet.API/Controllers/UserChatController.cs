using Microsoft.AspNetCore.Mvc;
using WebSocialNet.API.Authorization;
using WebSocialNet.Domain.Entities;
using WebSocialNet.Domain.Interfaces.IServices;
using WebSocialNet.Service;

namespace WebSocialNet.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserChatController : ControllerBase
    {
        private readonly IUserChatService _userChatService;

        public UserChatController(IUserChatService userChatService)
        {
            _userChatService = userChatService;
        }

        [CustomAuthorize]
        [HttpGet("search")]
        public IActionResult SearchUsers(string keyword) 
        {
            var user = (User)HttpContext.Items["User"];
            var foundedUsers = _userChatService.SearchUsers(keyword, user.Id);
            return Ok(foundedUsers);
        }

        [CustomAuthorize]
        [HttpPost()]
        public IActionResult CreateChat(string userId, string currentUserId) 
        {
            var createdChat = _userChatService.CreateChat(userId, currentUserId);
            return Ok(createdChat);
        }

        [CustomAuthorize]
        [HttpGet()]
        public IActionResult GetChat(string userId, string currentUserId)
        {
            var findedChat = _userChatService.GetChat(userId, currentUserId);
            return Ok(findedChat);
        }

        [CustomAuthorize]
        [HttpGet("recentchats")]
        public IActionResult GetRecentChats()
        {
            var user = (User)HttpContext.Items["User"];
            var recentChats = _userChatService.GetRecentChats(user.Id);
            return Ok(recentChats);
        }
    }
}
