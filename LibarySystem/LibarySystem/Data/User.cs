using System.ComponentModel.DataAnnotations;

namespace LibarySystem.Data
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime MembershipDate { get; set; }
    }
}
