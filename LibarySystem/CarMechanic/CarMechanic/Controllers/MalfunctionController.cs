using CarMechanic.Models;
using CarMechanic.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarMechanic.Controllers
{
    public class MalfunctionController : Controller
    {
        public readonly IMalfunctionRepository _malfunctionRepository;
        public MalfunctionController(IMalfunctionRepository malfunctionRepository)
        {
           _malfunctionRepository = malfunctionRepository;
        }
        public IActionResult AddMalfunction()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMalfunction(MalfunctionDTO malfunctionDTO) 
        {
            _malfunctionRepository.AddMalfunction(malfunctionDTO);
            return RedirectToAction("MalfunctionList");   
        }
        public IActionResult MalfunctionList()
        {
            var malfunctions = _malfunctionRepository.GetAllMalfunctions();
            return View(malfunctions);
        }

        public IActionResult DeleteMalfunction(int id)
        {
            var malfunctions = _malfunctionRepository.GetMalfunctionById(id);
            if (malfunctions == null)
            {
                return NotFound();
            }
            _malfunctionRepository.DeleteMalfunction(malfunctions);
            return RedirectToAction("MalfunctionList");
        }

        [HttpGet]

        public IActionResult UpdateMalFunction(int id)
        {
            var malfunctions = _malfunctionRepository.GetMalfunctionById(id);
            if (malfunctions == null)
            {
                return NotFound();
            }
            return View(malfunctions);
        }

        [HttpPost]
        public IActionResult UpdateMalFunction(MalfunctionDTO malfunctionDTO)
        {
            if (ModelState.IsValid)
            {
                _malfunctionRepository.UpdateMalfunction(malfunctionDTO);
                return RedirectToAction("MalfunctionList");
            }
            return View(malfunctionDTO);
        }
    }
}
