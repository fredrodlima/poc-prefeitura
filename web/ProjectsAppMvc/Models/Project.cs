using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectsAppMvc.Models
{
    public class Project
    {
        public int Id {get; set;}
        [Display(Name = "Nome")]
        public string Name {get; set;}
        [Display(Name = "Supervisor")]
        public int SupervisorId {get; set;}
        [Display(Name = "Data Início")]
        [DataType(DataType.Date)]
        public DateTime StartDate {get; set;}
        [Display(Name = "Data Fim")]
        [DataType(DataType.Date)]
        public DateTime? EndDate {get; set;}
        
    }

    public class ProjectViewModel 
    {
        public int Id {get; set;}
        [Display(Name = "Nome")]
        public string Name {get; set;}
        [Display(Name = "Supervisor")]
        public int SupervisorId {get; set;}
        
        [Display(Name = "Data Início")]
        [DataType(DataType.Date)]
        public DateTime StartDate {get; set;}
        [Display(Name = "Data Fim")]
        [DataType(DataType.Date)]
        public DateTime? EndDate {get; set;}
        [Display(Name = "Progresso")]
        [DisplayFormat(DataFormatString = "{0:p2}")]
        public double Progress {get; set;}
    }
}