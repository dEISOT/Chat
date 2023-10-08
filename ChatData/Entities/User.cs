using System.ComponentModel.DataAnnotations;

namespace ChatData.Entities
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}
