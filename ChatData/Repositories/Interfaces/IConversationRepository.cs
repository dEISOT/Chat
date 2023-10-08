using ChatData.Entities;

namespace ChatData.Repositories.Interfaces
{
    public interface IConversationRepository
    {
        public Task<Guid> CreateConversation(Conversation conversation);
        public Task<IEnumerable<Conversation>> GetAllUserConversations(Guid userId);
        public Task<Conversation> GetConversationById(Guid conversationId);
        public Task<bool> IsUserCreator(Guid conversationId, Guid userId);
        public Task DeleteConversationAsync(Guid conversationId);
        public Task<IEnumerable<Conversation>> SearchUserConversationsByNameAsync(string name, Guid userId);
    }
}
