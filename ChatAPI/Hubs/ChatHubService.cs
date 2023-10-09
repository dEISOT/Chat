using ChatAPI.Hubs;
using ChatCore.Mapping;
using Microsoft.AspNetCore.SignalR;

namespace ChatCore.Services
{
    public class ChatHubService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatHubService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
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
    }
}
