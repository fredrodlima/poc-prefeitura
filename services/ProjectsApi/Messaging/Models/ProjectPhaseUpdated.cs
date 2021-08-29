using ProjectsApi.Models;
using System;
namespace ProjectsApi.Messaging.Models
{
    public class ProjectPhaseUpdated
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public string Name { get; set; }

        public PhaseStatus Status { get; set; }
        public DateTime Timestamp { get; set; }
    }
}