using ChatCore.Services.Interfaces;
using ChatCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatModel.Request;

namespace ChatAPI.Controllers
{
    [Route("api/v1/members")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IConversationService _conversationService;
        private readonly ChatHubService _chatHubService;

        public MemberController(IMemberService memberService, IConversationService conversationService, ChatHubService chatHubService)
        {
            _memberService = memberService;
            _conversationService = conversationService;
            _chatHubService = chatHubService;
        }

        [HttpPost("conversations/{conversationId}")]
        public async Task<IActionResult> AddMember([FromBody] AddMembersRequestModel requestModel)
        {

            if (!Request.Headers.ContainsKey("User-Id"))
            {
                return BadRequest("UserId wasnt found. Please add new header parametr with name User-Id");
            }
            else if (await _memberService.IsUserInConverstationAsync(requestModel.ConversationId, new Guid(Request.Headers["User-Id"])))
            {
                await _memberService.AddUsersToConversationAsync(requestModel);
            }
            else
            {
                return Forbid();
            }
            return Ok();
        }

        [HttpDelete("conversations/{conversationId}")]
        public async Task<IActionResult> DeleteMember([FromBody] DeleteMemberRequestModel requestModel)
        {
            var test = Request.Headers["connectionId"];
            if (!Request.Headers.ContainsKey("User-Id"))
            {
                return BadRequest("UserId wasnt found. Please add new header parametr with name User-Id");
            }
            else if (await _conversationService.IsUserCreator(requestModel.ConversationId, new Guid(Request.Headers["User-Id"]))
                || Request.Headers["User-Id"] == requestModel.UserId.ToString())
            {
                await _memberService.DeleteMemberAsync(requestModel);
                await _chatHubService.DisconnectUserFromConversation(requestModel.ConversationId, requestModel.UserId);
                return NoContent();
            }
            else
            {
                return Forbid();
            }
        }
    }
}
