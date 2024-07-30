using CarMechanic.Models;
using System.ComponentModel.DataAnnotations;

namespace CarMechanic.Data
{
    public class ServiceReport
    {
        [Key]
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string CarBrand { get; set; }

        public string CarModel { get; set; }
        public string Description { get; set; }
        public DateTime DeliveryTime { get; set; }
        public int MalfunctionId { get; set; }
        public Malfunction? malfunction{ get; set; }
        public string? FilePath { get; set; }
    }
}
