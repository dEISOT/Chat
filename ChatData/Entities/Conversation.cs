using System.Text.Json.Serialization;

namespace ChatData.Entities
{
    public class Conversation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        [JsonIgnore]
        public virtual ICollection<Member> Members { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message> Messages { get; set; }
    }
}
