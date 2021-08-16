using Microsoft.EntityFrameworkCore;

namespace TaxCalculationApi.Models
{
    public class TaxesDbContext : DbContext
    {
        public TaxesDbContext(DbContextOptions<TaxesDbContext> options) : base (options)
        {
        }

        public DbSet<TaxRate> TaxRates {get; set;}
        public DbSet<Immobile> Immobiles {get; set;}
        public DbSet<TaxCalculation> TaxCalculations {get; set;}
    }
}