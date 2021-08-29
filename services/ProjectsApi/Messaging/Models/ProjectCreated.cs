using System;
namespace ProjectsApi.Messaging.Models
{
    public class ProjectCreated
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SupervisorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}