using ChatData.Entities;

namespace ChatCore.DTO
{
    public class MemberDTO
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; }
    }
}
