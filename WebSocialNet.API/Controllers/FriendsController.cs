using Microsoft.AspNetCore.Mvc;
using WebSocialNet.API.Authorization;
using WebSocialNet.Domain.Entities;
using WebSocialNet.Domain.Interfaces.IServices;

namespace WebSocialNet.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FriendsController : ControllerBase
    {
        private readonly IUserFriendService _userFriendService;

        public FriendsController(IUserFriendService userFriendService)
        {
            _userFriendService = userFriendService;
        }
        [CustomAuthorize]
        [HttpPost]
        public IActionResult CreateFriendship(string friendId) 
        {
            var user = (User)HttpContext.Items["User"];
            _userFriendService.CreateFriendship(user.Id, friendId);
            return Ok("Friendship Created");
        }

        [CustomAuthorize]
        [HttpGet]
        public IActionResult GetAllUserFriends() 
        {
            var user = (User)HttpContext.Items["User"];
            var allFriends = _userFriendService.GetAllFriendsFromUserId(user.Id);
            return Ok(allFriends);
        }
    }
}
