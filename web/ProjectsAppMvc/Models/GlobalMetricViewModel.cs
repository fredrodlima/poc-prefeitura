namespace ProjectsAppMvc.Models
{
    public class GlobalMetricViewModel
    {
        public int Id { get; set; }
        public int PhasesNotStarted { get; set; }
        public int PhasesInProgress { get; set; }
        public int PhasesCompleted { get; set; }
        public double Progress { get; set; }
    }
}
