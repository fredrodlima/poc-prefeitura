namespace ProjectsAppMvc.Models
{
    public class ProjectPhase
    {
        public int Id {get; set;}
        public int ProjectId {get; set;}

        public string Name {get; set;}

        public PhaseStatus Status {get; set;}   
    }

    public enum PhaseStatus
    {
        NotStarted,
        InProgress,
        Concluded
    }
}