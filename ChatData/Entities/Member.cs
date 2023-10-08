﻿namespace ChatData.Entities
{
    public class Member
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; }
    }
}
