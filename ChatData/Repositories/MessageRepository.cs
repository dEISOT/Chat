using ChatData.Contexts;
using ChatData.Entities;
using ChatData.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatData.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _db;

        public MessageRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddMessageAsync(Message message)
        {
            _db.Messages.Add(message);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetConversationMessages(Guid conversationId)
        {
            return await _db.Messages.Where(m => m.ConverstaionId == conversationId).ToListAsync();
        }
    }
}
