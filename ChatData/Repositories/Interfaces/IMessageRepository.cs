using ChatData.Entities;

namespace ChatData.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        public Task<IEnumerable<Message>> GetConversationMessages(Guid conversationId);
        public Task AddMessageAsync(Message message);
    }
}
