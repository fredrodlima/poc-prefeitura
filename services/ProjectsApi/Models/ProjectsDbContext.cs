using Microsoft.EntityFrameworkCore;

namespace ProjectsApi.Models{
    public class ProjectsDbContext: DbContext 
    {
        public ProjectsDbContext(DbContextOptions<ProjectsDbContext> options) : base (options)
        {
        }

        public DbSet<Project> Projects {get; set;}
        public DbSet<ProjectPhase> ProjectPhases {get; set;}
    }
}