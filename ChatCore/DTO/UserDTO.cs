using ChatData.Entities;

namespace ChatCore.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}
