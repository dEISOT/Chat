using ChatData.Contexts;
using ChatData.Entities;
using ChatData.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatData.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _db;

        public MemberRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddUsersToConversation(Guid conversationId, List<Guid> usersId)
        {
            foreach (Guid userId in usersId)
            {
                _db.Members
                    .Add(new Member
                    {
                        ConversationId = conversationId,
                        UserId = userId
                    });
            }
            await _db.SaveChangesAsync();
        }

        public async Task DeleteMemberAsync(Guid conversationId, Guid userId)
        {
            var member = await _db.Members.FirstOrDefaultAsync(m => m.ConversationId == conversationId
                            && m.UserId == userId);
            _db.Members.Remove(member);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Guid>> GetAllUsersInConversation(Guid conversationId)
        {
            return await _db.Members.Where(cu => cu.ConversationId == conversationId)
                                            .Select(cu => cu.UserId).ToListAsync();
        }

        public async Task<bool> IsUserInConverstationAsync(Guid conversationId, Guid userId)
        {
            return await _db.Members.AnyAsync(m => m.ConversationId == conversationId && m.UserId == userId);
        }
    }
}
