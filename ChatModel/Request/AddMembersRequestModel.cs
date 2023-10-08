namespace ChatModel.Request
{
    public class AddMembersRequestModel
    {
        public Guid ConversationId {  get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
