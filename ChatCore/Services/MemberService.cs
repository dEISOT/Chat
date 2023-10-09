using ChatCore.Services.Interfaces;
using ChatData.Repositories.Interfaces;
using ChatModel.Request;

namespace ChatCore.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository conversationUserRepository)
        {
            _memberRepository = conversationUserRepository;
        }

        public async Task AddUsersToConversationAsync(AddMembersRequestModel requestModel)
        {
            await _memberRepository.AddUsersToConversation(requestModel.ConversationId, requestModel.UserIds);
            //TODO : add users to group in hub 
        }

        public async Task DeleteMemberAsync(DeleteMemberRequestModel requestModel)
        {
            await _memberRepository.DeleteMemberAsync(requestModel.ConversationId, requestModel.UserId);
        }

        public async Task<IEnumerable<Guid>> GetAllUsersInConversationAsync(Guid conversationId)
        {
            return await _memberRepository.GetAllUsersInConversation(conversationId);
        }
        public async Task<bool> IsUserInConverstationAsync(Guid converstationId, Guid userId)
        {
            return await _memberRepository.IsUserInConverstationAsync(converstationId, userId);
        }
    }
}
