namespace ChatModel.Request
{
    public class DeleteMemberRequestModel
    {
        public Guid ConversationId { get; set; }
        public Guid UserId { get; set; }
    }
}
