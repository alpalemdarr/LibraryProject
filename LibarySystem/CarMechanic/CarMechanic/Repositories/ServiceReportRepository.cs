using CarMechanic.Data;
using CarMechanic.Models;
using Microsoft.EntityFrameworkCore;

namespace CarMechanic.Repositories
{
    public class ServiceReportRepository : IServiceReportRepository
    {
        private readonly DataContext _context;
        public ServiceReportRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public void AddService(ServiceReportDTO serviceReportDTO)
        {
            ServiceReport serviceReport = new ServiceReport()
            {
                CustomerName = serviceReportDTO.CustomerName,
                CarBrand = serviceReportDTO.CarBrand,
                CarModel = serviceReportDTO.CarModel,
                Description = serviceReportDTO.Description,
                DeliveryTime = serviceReportDTO.DeliveryTime,
                FilePath = serviceReportDTO.FilePath,
                MalfunctionId = serviceReportDTO.MalfunctionId,
                
                

            };
            _context.Reports.Add(serviceReport);
            _context.SaveChanges();
        }

        public void DeleteService(ServiceReportDTO serviceReportDTO)
        {
            var serviceReport = _context.Reports.FirstOrDefault(s => s.Id == serviceReportDTO.Id);
            if (serviceReport != null)
            {
                _context.Reports.Remove(serviceReport);
                _context.SaveChanges();
            }
        }

        public List<ServiceReportDTO> GetAllServices()
        {
            var serviceReport = _context.Reports.ToList();
            List<ServiceReportDTO> result = new List<ServiceReportDTO>();
            foreach(var item in serviceReport)
            {
                ServiceReportDTO service = new ServiceReportDTO()
                {
                    Id = item.Id,
                    CustomerName = item.CustomerName,
                    CarBrand = item.CarBrand,
                    CarModel = item.CarModel,
                    Description = item.Description,
                    DeliveryTime = item.DeliveryTime,
                    FilePath = item.FilePath,
                    MalfunctionId = item.MalfunctionId,

                   


                };
                if (item.MalfunctionId != null)
                {
                    var malfunctiona =_context.malfunctions.Where(x=>x.Id==item.MalfunctionId).FirstOrDefault();
                    MalfunctionDTO malF = new MalfunctionDTO()
                    {
                        Id = malfunctiona.Id,
                        MalfunctionName = malfunctiona.MalfunctionName
                        
                    };
                    service.malfunctionDTO = malF;
                }
                result.Add(service);
            }
            

            return result;
        }

        public ServiceReportDTO GetServiceReportById(int id)
        {
            var serviceReport = _context.Reports.Include(r => r.malfunction).FirstOrDefault(x => x.Id == id);
            if (serviceReport == null)
            {
                return null;
            }
            return new ServiceReportDTO
            {
                Id = serviceReport.Id,
                CustomerName = serviceReport.CustomerName,
                CarBrand = serviceReport.CarBrand,
                CarModel = serviceReport.CarModel,
                Description = serviceReport.Description,
                DeliveryTime = serviceReport.DeliveryTime,
                FilePath = serviceReport.FilePath,
                MalfunctionId = serviceReport.MalfunctionId,
                malfunctionDTO = new MalfunctionDTO
                {
                    Id = serviceReport.malfunction.Id,
                    MalfunctionName = serviceReport.malfunction.MalfunctionName
                }
            };
        }

        public void UpdateService(ServiceReportDTO serviceReportDTO)
        {
            var existingservices = _context.Reports.Include(r => r.malfunction).FirstOrDefault(x => x.Id == serviceReportDTO.Id);

            if (existingservices != null)
            {
                if (serviceReportDTO.File == null)
                {
                    serviceReportDTO.FilePath = existingservices.FilePath;
                }
                
                existingservices.CustomerName = serviceReportDTO.CustomerName;
                existingservices.CarBrand = serviceReportDTO.CarBrand;
                existingservices.CarModel = serviceReportDTO.CarModel;
                existingservices.Description = serviceReportDTO.Description;
                existingservices.DeliveryTime = serviceReportDTO.DeliveryTime;
                existingservices.FilePath = serviceReportDTO.FilePath;
                existingservices.MalfunctionId = serviceReportDTO.MalfunctionId;


                // Book ve User entity'lerini yeniden yükle
                existingservices.malfunction = _context.malfunctions.Where(x => x.Id == serviceReportDTO.MalfunctionId).FirstOrDefault();
                
                _context.Reports.Update(existingservices);

                _context.SaveChanges();
            }
        }
    }
    }

