using ChatData.Contexts;
using ChatData.Entities;
using ChatData.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatData.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly ApplicationDbContext _db;

        public ConversationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Guid> CreateConversation(Conversation conversation)
        {
            _db.Conversations.Add(conversation);
            await _db.SaveChangesAsync();
            return conversation.Id;
        }

        public async Task DeleteConversationAsync(Guid conversationId)
        {
            var conversation = await _db.Conversations.FirstOrDefaultAsync(c => c.Id == conversationId);
            _db.Conversations.Remove(conversation);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Conversation>> GetAllUserConversations(Guid userId)
        {
            return await _db.Members.Where(c => c.UserId == userId).Select(cu => cu.Conversation).ToListAsync();
        }

        public async Task<Conversation> GetConversationById(Guid conversationId)
        {
            return await _db.Conversations.Where(c => c.Id == conversationId).FirstOrDefaultAsync();
        }

        public async Task<bool> IsUserCreator(Guid conversationId, Guid userId)
        {
            return await _db.Conversations.AnyAsync(c => c.Id == conversationId && c.CreatorId == userId);
        }

        public async Task<IEnumerable<Conversation>> SearchUserConversationsByNameAsync(string name, Guid userId)
        {
            return await _db.Conversations
                .Where(c => c.Members.Any(m => m.UserId == userId)
                    && c.Name.Contains(name))
                .ToListAsync();
        }
    }
}
