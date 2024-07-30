using System.ComponentModel.DataAnnotations;

namespace LibarySystem.Models
{
    public class UserDTO
    {
       
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime MembershipDate { get; set; }
    }
}
