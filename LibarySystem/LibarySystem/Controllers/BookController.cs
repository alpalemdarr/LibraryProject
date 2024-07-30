using LibarySystem.Models;
using LibarySystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibarySystem.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBook(BookDTO book)
        {
            if (book.File != null && book.File.Length > 0)
            {
                DirectoryInfo fileType = new DirectoryInfo(book.File.FileName);
                string fileName = Guid.NewGuid().ToString() + fileType.Extension;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    book.File.CopyToAsync(stream);
                }
                book.FilePath = "/img/" + fileName;
            }
            else
            {
                ViewBag.Message = "Lütfen bir dosya seçin.";
            }
            _bookRepository.AddBook(book);
            return RedirectToAction("BookList");
        }
        public IActionResult BookList()
        {
            var books = _bookRepository.GetAllBooks();
            return View(books);
        }

        [HttpGet]
        public IActionResult DeleteBook(int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            if (book == null)
            {
                return NotFound();
            }
            _bookRepository.DeleteBook(book);
            return RedirectToAction("BookList");

        }
        [HttpGet]
        public IActionResult UpdateBook(int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);

        }
        [HttpPost]
        public IActionResult UpdateBook(BookDTO book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.UpdateBook(book);
                return RedirectToAction("BookList");
            }
            return View(book);
        }
    }
}
