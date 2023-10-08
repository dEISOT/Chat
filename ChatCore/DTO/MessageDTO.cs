using ChatData.Entities;
using System.Text.Json.Serialization;

namespace ChatCore.DTO
{
    public class MessageDTO
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string MessageText { get; set; }
        public DateTime SentDateTime { get; set; }
        public Guid ConverstaionId { get; set; }
        [JsonIgnore]
        public Conversation Conversation { get; set; }
    }
}