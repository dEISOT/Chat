using AutoMapper;
using ChatCore.DTO;
using ChatCore.Services.Interfaces;
using ChatData.Entities;
using ChatData.Repositories.Interfaces;
using ChatModel.Request;

namespace ChatCore.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public ConversationService(IConversationRepository conversationRepository, IMemberService memberService, IMapper mapper)
        {
            _conversationRepository = conversationRepository;
            _memberService = memberService;
            _mapper = mapper;
        }

        public async Task<Guid> CreateConversation(CreateConversationRequest requestModel)
        {
            Conversation conversation = new Conversation
            {
                Name = requestModel.Name,
                CreatorId = requestModel.CreatorId,
                CreationDate = DateTime.Today
            };
            var result = await _conversationRepository.CreateConversation(conversation);
            requestModel.UserIds.Add(requestModel.CreatorId);
            await _memberService.AddUsersToConversationAsync(new AddMembersRequestModel { UserIds = requestModel.UserIds, ConversationId = result});
            return result;

        }

        public async Task<ConversationDTO> GetConversationByIdAsync(Guid conversationId)
        {
            var conversation = await _conversationRepository.GetConversationById(conversationId);
            return _mapper.Map<ConversationDTO>(conversation);
        }


        public async Task<IEnumerable<ConversationDTO>> GetAllUserConversations(Guid userId)
        {
            var conversations = await _conversationRepository.GetAllUserConversations(userId);
            return _mapper.Map<IEnumerable<ConversationDTO>>(conversations);
        }
        public async Task<bool> IsUserCreator(Guid conversationId, Guid userId)
        {
            return await _conversationRepository.IsUserCreator(conversationId, userId);
        }
        public async Task DeleteConversationAsync(Guid conversationId)
        {
            await _conversationRepository.DeleteConversationAsync(conversationId);
        }

        public async Task<IEnumerable<ConversationDTO>> SearchUserConversationsByNameAsync(SearchConversationRequest requestModel)
        {
            var conversations = await _conversationRepository.SearchUserConversationsByNameAsync(requestModel.Name, requestModel.UserId);
            return _mapper.Map<IEnumerable<ConversationDTO>>(conversations);
        }
    }
}
