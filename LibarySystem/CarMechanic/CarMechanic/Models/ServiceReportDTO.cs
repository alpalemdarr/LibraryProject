namespace CarMechanic.Models
{
    public class ServiceReportDTO
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string CarBrand  { get; set; }

        public string CarModel { get; set; }
        public string Description { get; set; }
        public DateTime DeliveryTime { get; set; }
        public int MalfunctionId {  get; set; }
        public MalfunctionDTO? malfunctionDTO { get; set; }
        public IFormFile? File { get; set; }
        public string? FilePath { get; set; }

    }
}
