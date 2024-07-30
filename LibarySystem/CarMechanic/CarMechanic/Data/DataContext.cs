using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CarMechanic.Data
{
    public class DataContext :DbContext
    
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {

        }
        public DbSet<ServiceReport> Reports { get; set; }
        public DbSet<Malfunction> malfunctions { get; set; }
    }
}
