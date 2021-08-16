using Microsoft.EntityFrameworkCore;

namespace CitizensApi.Models
{
    public class CitizensDbContext : DbContext
    {
        public CitizensDbContext(DbContextOptions<CitizensDbContext> options) : base (options)
        {
        }

        public DbSet<Citizen> Citizens {get; set;}
        public DbSet<CitizenType> CitizenTypes {get; set;}
    }
}