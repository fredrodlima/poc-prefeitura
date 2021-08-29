using System.ComponentModel.DataAnnotations;

namespace ProjectsApi.Models {
    public class ProjectPhase {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public string Name { get; set; }

        public PhaseStatus Status { get; set; }
    }

    public enum PhaseStatus
    {
        [Display(Name = "N�o iniciada")]
        NotStarted,
        [Display(Name = "Em progresso")]
        InProgress,
        [Display(Name = "Conclu�da")]
        Completed
    }
}