using ChatAPI.Hubs;
using ChatCore.Services;
using ChatCore.Services.Interfaces;
using ChatModel.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Controllers
{
    [Route("api/v1/conversations")]
    [ApiController]
    public class ConversationController : ControllerBase
    {

        private readonly IConversationService _conversationService;
        private readonly ChatHubService _chatHubService;

        public ConversationController(IConversationService conversationService, ChatHubService chatHubService)
        {
            _conversationService = conversationService;
            _chatHubService = chatHubService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!Request.Headers.ContainsKey("User-Id"))
            {
                return BadRequest("UserId wasnt found. Please add new header parametr with name User-Id");
            }
            var userId = new Guid(Request.Headers["User-Id"]);
            var conversations = await _conversationService.GetAllUserConversations(userId);
            return Ok(conversations);
        }

        [HttpGet("{conversationId}")]
        public async Task<IActionResult> GetById(Guid conversationId)
        {
            var conversation = await _conversationService.GetConversationByIdAsync(conversationId);
            await _chatHubService.ConnectUserToConversation(conversationId, new Guid(Request.Headers["User-Id"]));
            return Ok(conversation);
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> Search(string name)
        {
            if (!Request.Headers.ContainsKey("User-Id"))
            {
                return BadRequest("UserId wasnt found. Please add new header parametr with name User-Id");
            }
            SearchConversationRequest requestModel = new SearchConversationRequest {
                Name = name,
                UserId = new Guid(Request.Headers["User-Id"])
            };
            var conversation = await _conversationService.SearchUserConversationsByNameAsync(requestModel);
            return Ok(conversation);
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateConverstaion([FromBody] CreateConversationRequest requestModel)
        {
            var conversationId = await _conversationService.CreateConversation(requestModel);
            return Ok(conversationId);
        }

        [HttpDelete("{conversationId}")]
        public async Task<IActionResult> DeleteConversation(Guid conversationId)
        {
            if (!Request.Headers.ContainsKey("User-Id"))
            {
                return BadRequest("UserId wasnt found. Please add new header parametr with name User-Id");
            }
            else if (await _conversationService.IsUserCreator(conversationId, new Guid(Request.Headers["User-Id"])))
            {
                await _conversationService.DeleteConversationAsync(conversationId);
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
        }
    }
}
