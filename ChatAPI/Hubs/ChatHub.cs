using ChatCore.Mapping;
using ChatCore.Services;
using ChatCore.Services.Interfaces;
using ChatData.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace ChatAPI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IConversationService _converstaionService;
        private readonly IMemberService _memberService;
        private readonly IMessageService _messageService;
        private readonly ChatHubService _chatHubService;

        public ChatHub(IConversationService converstaionService, IMemberService memberService, IMessageService messageService, ChatHubService chatHubService)
        {
            _converstaionService = converstaionService;
            _memberService = memberService;
            _messageService = messageService;
            _chatHubService = chatHubService;
        }

        public async Task SendMessage(Guid conversationId, string message, Guid userId)
        {

            var conversationUsers = await _memberService.GetAllUsersInConversationAsync(conversationId);


            foreach (Guid id in conversationUsers)
            {
                if (ConnectionMapping.TryGetValue(id, out var connectionIds))
                {
                    //TODO: send message
                    foreach (var connectionId in connectionIds)
                    {
                        await Groups.AddToGroupAsync(connectionId, conversationId.ToString());
                    }
                }
                //TODO: add message to db
            }
            await Clients.Group(conversationId.ToString()).SendAsync("Send", message + " " + Context.ConnectionId);
            await _messageService.AddMessageAsync(
                   new Message
                   {
                       SenderId = userId,
                       MessageText = message,
                       ConverstaionId = conversationId,
                       SentDateTime = DateTime.UtcNow
                   });
        }
        
        public async Task DisconnectFromConversation(Guid conversationId, Guid userId)
        {
            await _chatHubService.DisconnectUserFromConversation(conversationId, userId);
        }

        public async Task ConnectToConversation(Guid conversationId, Guid userId)
        {
            await _chatHubService.DisconnectUserFromConversation(conversationId, userId);
        }
        public override async Task OnConnectedAsync()
        {
            var context = Context.GetHttpContext();

            if (!context.Request.Headers.ContainsKey("User-Id"))
            {
                throw new HubException("UserId wasnt found. Please add new header parametr with name User-Id");
            }

            await base.OnConnectedAsync();
            Guid userId = new Guid(context.Request.Headers["User-Id"]);
            ConnectionMapping.Add(userId, Context.ConnectionId);

            await this.Clients.All.SendAsync("BroadcastMessage", $"{Context.ConnectionId} are now connected!");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var context = Context.GetHttpContext();

            if (!context.Request.Headers.ContainsKey("User-Id"))
            {
                throw new HubException("UserId wasnt found. Please add new header parametr with name User-Id");
            }
            await base.OnDisconnectedAsync(exception);
            Guid userId = new Guid(context.Request.Headers["User-Id"]);
            ConnectionMapping.Remove(userId, Context.ConnectionId);
        }
    }
}
