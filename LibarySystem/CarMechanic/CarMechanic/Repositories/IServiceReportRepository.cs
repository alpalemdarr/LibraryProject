using CarMechanic.Models;

namespace CarMechanic.Repositories
{
    public interface IServiceReportRepository
    {
        void AddService(ServiceReportDTO serviceReportDTO);
        void UpdateService(ServiceReportDTO serviceReportDTO);
        void DeleteService(ServiceReportDTO serviceReportDTO);
        List<ServiceReportDTO> GetAllServices();
        ServiceReportDTO GetServiceReportById(int id);
    }
}
