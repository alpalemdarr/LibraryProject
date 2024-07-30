using LibarySystem.Data;
using LibarySystem.Models;
using LibarySystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibarySystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(UserDTO user) 
        {
            _userRepository.AddUser(user);
            return RedirectToAction("UserList");
        }
        public IActionResult UserList() 
        {
            var users = _userRepository.GetAllUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult DeleteUser(int userId)
        {
           var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            _userRepository.DeleteUser(user);
            return RedirectToAction("UserList");

        }
        [HttpGet]
        public IActionResult UpdateUser(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);

        }
        [HttpPost]
        public IActionResult UpdateUser(UserDTO _user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.UpdateUser(_user);
                return RedirectToAction("UserList");
            }
            return View(_user);
        }
    }
}
