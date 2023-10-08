using AutoMapper;
using ChatCore.DTO;
using ChatCore.Services.Interfaces;
using ChatData.Entities;
using ChatData.Repositories.Interfaces;

namespace ChatCore.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task AddMessageAsync(Message message)
        {
            await _messageRepository.AddMessageAsync(message);
        }

        public async Task<IEnumerable<MessageDTO>> GetConversationMessages(Guid conversationId)
        {
            var messages = await _messageRepository.GetConversationMessages(conversationId);
            return _mapper.Map<IEnumerable<MessageDTO>>(messages);
        }
    }
}
