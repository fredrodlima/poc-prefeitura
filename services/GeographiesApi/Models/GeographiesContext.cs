using Microsoft.EntityFrameworkCore;

namespace GeographiesApi.Models
{
    public class GeographiesContext : DbContext
    {
        public GeographiesContext(DbContextOptions<GeographiesContext> options) : base(options)
        {
        }
        public DbSet<AdministrativeDivision> AdministrativeDivisions { get; set; }
        //public DbSet<CitizenLocalitie> AdministrativeDivisions { get; set; }
        //public DbSet<CrimeOccurrence> AdministrativeDivisions { get; set; }
    }
}