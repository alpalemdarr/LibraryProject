using LibarySystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibarySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository _bookRepository;
        public HomeController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public IActionResult Index()
        {
            var books = _bookRepository.GetAllBooks();
            return View(books);
        }
    }
}
