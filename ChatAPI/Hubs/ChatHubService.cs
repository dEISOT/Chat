using ChatAPI.Hubs;
using ChatCore.Mapping;
using ChatCore.Services.Interfaces;
using ChatData.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace ChatCore.Services
{
    public class ChatHubService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IMemberService _memberService;

        public ChatHubService(IHubContext<ChatHub> hubContext, IMemberService memberService)
        {
            _hubContext = hubContext;
            _memberService = memberService;
        }

        public async Task DisconnectUserFromConversation(Guid conversationId, Guid userId)
        {
            if (ConnectionMapping.TryGetValue(userId, out var connectionIds))
            {
                foreach (var connectionId in connectionIds)
                {
                    await _hubContext.Groups.RemoveFromGroupAsync(connectionId, conversationId.ToString());
                }
            }
        }
        public async Task ConnectUserToConversation(Guid conversationId, Guid userId)
        {
            var conversationUsers = await _memberService.GetAllUsersInConversationAsync(conversationId);

            foreach (Guid id in conversationUsers)
            {
                if (ConnectionMapping.TryGetValue(id, out var connectionIds))
                {
                    
                    foreach (var connectionId in connectionIds)
                    {
                        await _hubContext.Groups.AddToGroupAsync(connectionId, conversationId.ToString());
                    }
                }

            }
            await _hubContext.Clients.Group(conversationId.ToString()).SendAsync("BroadcastMessage", $"{userId}" + " " + "connected to conversation");
        }
    }
}
