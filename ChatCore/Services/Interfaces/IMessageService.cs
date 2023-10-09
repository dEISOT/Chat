using ChatCore.DTO;
using ChatData.Entities;

namespace ChatCore.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<IEnumerable<MessageDTO>> GetConversationMessages(Guid conversationId);
        public Task<Guid> AddMessageAsync(Message message);
    }
}
