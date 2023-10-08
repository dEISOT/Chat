using ChatModel.Request;

namespace ChatCore.Services.Interfaces
{
    public interface IMemberService
    {
        public Task AddUsersToConversationAsync(AddMembersRequestModel requestModel);
        public Task<IEnumerable<Guid>> GetAllUsersInConversationAsync(Guid conversationId);
        public Task<bool> IsUserInConverstationAsync(Guid converstationId, Guid userId);
        public Task DeleteMemberAsync(DeleteMemberRequestModel requestModel);
    }
}
