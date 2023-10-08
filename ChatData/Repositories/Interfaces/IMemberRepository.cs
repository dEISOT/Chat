namespace ChatData.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        public Task AddUsersToConversation(Guid conversationId, List<Guid> usersId);
        public Task<IEnumerable<Guid>> GetAllUsersInConversation(Guid conversationId);
        public Task<bool> IsUserInConverstationAsync(Guid conversationId, Guid userId);
        public Task DeleteMemberAsync(Guid conversationId, Guid userId);
    }
}
