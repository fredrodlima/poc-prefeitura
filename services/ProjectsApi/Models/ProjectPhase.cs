namespace ProjectsApi.Models {
    public class ProjectPhase {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public string Name { get; set; }

        public Status Status { get; set; }
    }

    public enum Status {
        NotInitied,
        InProgress,
        Finished
    }
}