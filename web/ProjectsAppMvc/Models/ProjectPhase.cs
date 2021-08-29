using System.ComponentModel.DataAnnotations;

namespace ProjectsAppMvc.Models
{
    public class ProjectPhase
    {
        public int Id {get; set;}
        [Display(Name = "Projeto")]
        public int ProjectId {get; set;}
        [Display(Name = "Nome")]
        public string Name {get; set;}
        [Display(Name = "Status")]
        public PhaseStatus Status {get; set;}   
    }

    public enum PhaseStatus
    {
        [Display(Name = "Não iniciada")]
        NotStarted,
        [Display(Name = "Em progresso")]
        InProgress,
        [Display(Name = "Concluída")]
        Completed
    }
}