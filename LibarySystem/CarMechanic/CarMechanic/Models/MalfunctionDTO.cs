namespace CarMechanic.Models
{
    public class MalfunctionDTO
    {
        public int Id { get; set; }
        public string MalfunctionName { get; set; }
        public List<ServiceReportDTO>? services { get; set; }

        
    }
}
