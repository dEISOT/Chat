using ChatCore.DTO;
using ChatModel.Request;

namespace ChatCore.Services.Interfaces
{
    public interface IConversationService
    {
        public Task<ConversationDTO> GetConversationByIdAsync(Guid conversationId);
        public Task<IEnumerable<ConversationDTO>> SearchUserConversationsByNameAsync(SearchConversationRequest requestModel);
        public Task<Guid> CreateConversation(CreateConversationRequest requestModel);
        public Task<IEnumerable<ConversationDTO>> GetAllUserConversations(Guid userId);
        public Task<bool> IsUserCreator(Guid conversationId, Guid userId);
        public Task DeleteConversationAsync(Guid conversationId);
    }
}
