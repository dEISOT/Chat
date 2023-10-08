using ChatData.Entities;
using System.Text.Json.Serialization;

namespace ChatCore.DTO
{
    public class ConversationDTO
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
