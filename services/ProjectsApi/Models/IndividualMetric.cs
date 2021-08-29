using System.ComponentModel.DataAnnotations;

namespace ProjectsApi.Models
{
    public class IndividualMetric
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int PhasesNotStarted { get; set; }
        public int PhasesInProgress { get; set; }
        public int PhasesCompleted { get; set; }
        public double Progress { get; set; }
    }
}