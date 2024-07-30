using LibarySystem.Data;
using LibarySystem.Models;
using LibarySystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace LibarySystem.Controllers
{
    public class BorrowingController : Controller
    {
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        public BorrowingController(IBookRepository bookRepository , IBorrowingRepository borrowingRepository , IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _borrowingRepository = borrowingRepository;
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult AddBorrow()
        {
          
            var books = _bookRepository.GetAllBooks();
            ViewBag.BookItems = new SelectList(books, "BookId", "BookName");

           
            var users = _userRepository.GetAllUsers();
            ViewBag.UserItems = new SelectList(users, "UserId", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult AddBorrow(BorrowingsDTO borrow)
        {
            if (ModelState.IsValid)
            {
                _borrowingRepository.AddBorrowing(borrow);
                return RedirectToAction("BorrowList", "Borrowing");
            }
            

            // Kitap ve kullanıcı listesini yeniden ekleyin
            var books = _bookRepository.GetAllBooks();
            ViewBag.BookItems = new SelectList(books, "BookId", "BookTitle");
            var users = _userRepository.GetAllUsers();
            ViewBag.UserItems = new SelectList(users, "UserId", "Name");

            return View(borrow);
        }
        [HttpGet]
        public IActionResult BorrowList() 
        {
            var borrow = _borrowingRepository.GetAllBorrowings();
            
            return View(borrow);
        }

        [HttpGet]
        public IActionResult DeleteBorrow(int borrowId)
        {
            var borrow = _borrowingRepository.GetBorrowById(borrowId);
            if (borrow == null)
            {
                return NotFound();
            }
            _borrowingRepository.DeleteBorrowing(borrow);
            return RedirectToAction("BorrowList");

        }
        [HttpGet]
        public IActionResult UpdateBorrow(int borrowId)
        {
            var borrow = _borrowingRepository.GetBorrowById(borrowId);
            if (borrow == null)
            {
                return NotFound();
            }

            var books = _bookRepository.GetAllBooks();
            ViewBag.BookItems = new SelectList(books, "BookId", "BookTitle");

            var users = _userRepository.GetAllUsers();
            ViewBag.UserItems = new SelectList(users, "UserId", "Name");

            return View(borrow);
        }
        [HttpPost]
        public IActionResult UpdateBorrow(BorrowingsDTO _borrow)
        {
            if (ModelState.IsValid)
            {
                _borrowingRepository.UpdateBorrowing(_borrow);
                return RedirectToAction("BorrowList");
            }
            return View(_borrow);
        }
    }
}
