using Microsoft.EntityFrameworkCore;  
using CityMonitoringAppMvc.Entities;  
  
namespace CityMonitoringAppMvc  
{  
    public class GeographiesDbContext : DbContext  
    {  
        public DbSet<CitizenLocality> CitizenLocalities { get; set; }  
        public DbSet<AdministrativeDivision> AdminitrativeDivisions { get; set; }
        public DbSet<CrimeOccurrence> CrimeOcurrences { get; set; }
  
        public GeographiesDbContext(DbContextOptions options) : base(options) { }  
    }  
} 