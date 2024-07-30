using LibarySystem.Models;

namespace LibarySystem.Repositories
{
    public interface IBookRepository
    {
        void AddBook(BookDTO book);
        List<BookDTO> GetAllBooks();
        BookDTO GetBookById(int bookId);
        void UpdateBook(BookDTO book);
        void DeleteBook(BookDTO book);
    }
}
