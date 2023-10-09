using ChatCore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    [Route("api/v1/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("conversations/{conversationId}")]
        public async Task<IActionResult> GetConversationMessages(Guid conversationId)
        {
            var messages = await _messageService.GetConversationMessages(conversationId);
            return Ok(messages);
        }
    }
}
