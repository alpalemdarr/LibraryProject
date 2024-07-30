using LibarySystem.Data;
using LibarySystem.Models;

namespace LibarySystem.Repositories
{
    public class BookRepository : IBookRepository
    {
        private List<BookDTO> _books = new List<BookDTO>();
        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }
        public void AddBook(BookDTO _book)
        {
            Book book = new Book();

            book.BookName = _book.BookName;
            book.BookTitle = _book.BookTitle;
            book.Author = _book.Author;
            book.FilePath = _book.FilePath;
            book.PublishedDate = _book.PublishedDate;

            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void DeleteBook(BookDTO _book)
        {
            var book = _context.Books.Find(_book.BookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public List<BookDTO> GetAllBooks()
        {
            var books = _context.Books.ToList(); // Veritabanından tüm arabaları çekmek
            var bookDTO = books.Select(book => new BookDTO
            {
                BookId = book.BookId,
                BookName = book.BookName,
                Author = book.Author,
                PublishedDate = book.PublishedDate,
                FilePath = book.FilePath,
                BookTitle = book.BookTitle
                
            }).ToList();

            return bookDTO;
        }

        public BookDTO GetBookById(int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);

            if (book == null)
            {
                return null; // Kullanıcı bulunamazsa null döndür
            }

            return new BookDTO
            {
                BookId = book.BookId,
                BookName = book.BookName,
                Author = book.Author,
                PublishedDate = book.PublishedDate,
                FilePath = book.FilePath,
                BookTitle = book.BookTitle
            };
        }

        public void UpdateBook(BookDTO _book)
        {
            var book = _context.Books.Find(_book.BookId);
            if (book != null)
            {
                // Eğer File değeri null gelirse, mevcut FilePath'i koru
                if (_book.File == null)
                {
                    _book.FilePath = book.FilePath;
                }

                book.BookName = _book.BookName;
                book.BookTitle = _book.BookTitle;
                book.Author = _book.Author;
                book.FilePath = _book.FilePath;
                book.PublishedDate = _book.PublishedDate;

                _context.Books.Update(book);
                _context.SaveChanges();
            }
        }
    }
}
