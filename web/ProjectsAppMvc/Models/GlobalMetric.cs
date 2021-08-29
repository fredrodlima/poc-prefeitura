using System.ComponentModel.DataAnnotations;

namespace ProjectsAppMvc.Models
{
    public class GlobalMetric
    {
        public int Id { get; set; }
        [Display(Name = "Fases não iniciadas")]
        public int PhasesNotStarted { get; set; }
        [Display(Name = "Fases em progresso")]
        public int PhasesInProgress { get; set; }
        [Display(Name = "Fases concluídas")]
        public int PhasesCompleted { get; set; }
        [Display(Name = "Progresso (%)")]
        public double Progress { get; set; }
    }
}
