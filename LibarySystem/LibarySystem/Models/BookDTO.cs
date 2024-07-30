

using Microsoft.AspNetCore.Http;

namespace LibarySystem.Models
{
    public class BookDTO
    {
       
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public IFormFile? File { get; set; }
        public string? FilePath { get; set; }
        public DateTime PublishedDate { get; set; }

    }
}
