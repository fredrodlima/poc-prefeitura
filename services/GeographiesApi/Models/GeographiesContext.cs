using Microsoft.EntityFrameworkCore;

namespace GeographiesApi.Models
{
    public class GeographiesContext : DbContext
    {
        public GeographiesContext(DbContextOptions<GeographiesContext> options) : base(options)
        {
        }
        public DbSet<AdministrativeDivision> AdministrativeDivisions { get; set; }
        public DbSet<AdministrativeDivisionLevel> AdministrativeDivisionLevels { get; set; }
        public DbSet<CitizenLocality> CitizenLocalities { get; set; }
        public DbSet<CrimeOccurrence> CrimeOccurrences { get; set; }
    }
}