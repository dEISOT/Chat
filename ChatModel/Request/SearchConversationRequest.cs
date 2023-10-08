namespace ChatModel.Request
{
    public class SearchConversationRequest
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}
