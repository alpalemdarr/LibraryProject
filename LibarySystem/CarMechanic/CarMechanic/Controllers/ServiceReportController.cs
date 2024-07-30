using CarMechanic.Data;
using CarMechanic.Models;
using CarMechanic.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarMechanic.Controllers
{
    public class ServiceReportController : Controller
    {
        public readonly IMalfunctionRepository _malfunctionRepository;
        public readonly IServiceReportRepository _serviceReportRepository;

        

        
        public ServiceReportController(IMalfunctionRepository malfunctionRepository, IServiceReportRepository serviceReportRepository)
        {
            _malfunctionRepository = malfunctionRepository;
            _serviceReportRepository = serviceReportRepository;
        }
        public IActionResult AddService()
        {
            var malfunction = _malfunctionRepository.GetAllMalfunctions();
            ViewBag.Malfunction = malfunction;

            return View();
        }
        [HttpPost]
        public IActionResult AddService(ServiceReportDTO serviceReportDTO )
        {

            if (ModelState.IsValid)
            {
                if (serviceReportDTO.File != null && serviceReportDTO.File.Length > 0)
                {
                    DirectoryInfo fileType = new DirectoryInfo(serviceReportDTO.File.FileName);
                    string fileName = Guid.NewGuid().ToString() + fileType.Extension;

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ımage", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        serviceReportDTO.File.CopyToAsync(stream);
                    }
                    serviceReportDTO.FilePath = "/ımage/" + fileName;
                }
                else
                {
                    ViewBag.Message = "Lütfen Bir Dosya Seçiniz";
                }
                _serviceReportRepository.AddService(serviceReportDTO);
                return RedirectToAction("ServicesList");   
            }

            var malfunction = _malfunctionRepository.GetAllMalfunctions();
            ViewBag.Malfunction = malfunction;

            return View(serviceReportDTO);

        }

        public IActionResult ServicesList()
        {
            var services = _serviceReportRepository.GetAllServices();
            return View(services);
        }

        public IActionResult DeleteService(int id) 
        {
            var services = _serviceReportRepository.GetServiceReportById(id);
            if (services == null)
            {
                return NotFound();
            }
            _serviceReportRepository.DeleteService(services);
            return RedirectToAction("ServicesList");
        }
        [HttpGet]
        public IActionResult UpdateService(int id) 
        {
            var services = _serviceReportRepository.GetServiceReportById(id);
            if (services == null)
            {
                return NotFound();
            }
            var malfunction = _malfunctionRepository.GetAllMalfunctions();
            ViewBag.Malfunction = malfunction;

            return View(services);

        }
        [HttpPost]
        public IActionResult UpdateService(ServiceReportDTO serviceReportDTO)
        {
           if (ModelState.IsValid)
            {
                if (serviceReportDTO.File != null && serviceReportDTO.File.Length > 0)
                {
                    DirectoryInfo fileType = new DirectoryInfo(serviceReportDTO.File.FileName);
                    string fileName = Guid.NewGuid().ToString() + fileType.Extension;

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ımage", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        serviceReportDTO.File.CopyToAsync(stream);
                    }
                    serviceReportDTO.FilePath = "/ımage/" + fileName;
                }
                else
                {
                    ViewBag.Message = "Lütfen Bir Dosya Seçiniz";
                }
                _serviceReportRepository.UpdateService(serviceReportDTO);
                return RedirectToAction("ServicesList");
            }
           return View(serviceReportDTO);

        }
    }
}
