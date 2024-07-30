using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace LibarySystem.Models
{
    public class BorrowingsDTO
    {
        public int BorrowingsId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime BorrowingDate { get; set; }

        public DateTime ReturnTime { get; set; }

        public BookDTO? Book { get; set; }
        public UserDTO? User { get; set; }
    }
}
