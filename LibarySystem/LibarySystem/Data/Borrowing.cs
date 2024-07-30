using LibarySystem.Models;
using System.ComponentModel.DataAnnotations;

namespace LibarySystem.Data
{
    public class Borrowing
    {
        [Key]
        public int BorrowingsId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime BorrowingDate { get; set; }
        public DateTime ReturnTime { get; set; }

        public Book? Book { get; set; }
        public User? User { get; set; }
    }
}
