using Microsoft.AspNetCore.Mvc;
using WebSocialNet.API.Authorization;
using WebSocialNet.Domain.DTOs.MessageDTOs;
using WebSocialNet.Domain.Entities;
using WebSocialNet.Domain.Interfaces.IServices;

namespace WebSocialNet.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [CustomAuthorize]
        [HttpPost("toChatId")]
        public IActionResult SendMessage(string toChatId, SendMessageDTO message) 
        {
            var user = (User)HttpContext.Items["User"];
            message.SenderId = user.Id;          
            
            var sendedMessage = _messageService.SendMessage(toChatId, message, user.Name);
            return Ok(sendedMessage);
        }


        [CustomAuthorize]
        [HttpGet("chatId")]
        public IActionResult GetAllMessagesFromChat(string chatId)
        {
            var allChatMessages = _messageService.GetMessagesFromChat(chatId);
            return Ok(allChatMessages);
        }
    }
}
