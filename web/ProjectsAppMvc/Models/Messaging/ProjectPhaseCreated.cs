using System;

namespace ProjectsAppMvc.Models.Messaging
{
    public class ProjectPhaseCreated
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public string Name { get; set; }

        public PhaseStatus Status { get; set; }
        public DateTime Timestamp { get; set; }
    }
}