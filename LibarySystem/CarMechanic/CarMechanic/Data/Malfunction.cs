using System.ComponentModel.DataAnnotations;

namespace CarMechanic.Data
{
    public class Malfunction
    {
        [Key]
        public int Id { get; set; }
        public string MalfunctionName { get; set; }
        public List<ServiceReport> Reports { get; set; }
    }
}
