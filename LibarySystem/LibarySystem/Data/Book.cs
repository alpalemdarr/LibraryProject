using System.ComponentModel.DataAnnotations;

namespace LibarySystem.Data
{
    public class Book
    {

        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string? FilePath { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
