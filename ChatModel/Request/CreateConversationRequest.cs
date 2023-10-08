namespace ChatModel.Request
{
    public class CreateConversationRequest
    {
        public string Name {  get; set; }
        public Guid CreatorId { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
