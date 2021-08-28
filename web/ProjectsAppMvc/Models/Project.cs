using System;

namespace ProjectsAppMvc.Models
{
    public class Project 
    {
        public int Id {get; set;}

        public string Name {get; set;}

        public int SupervisorId {get; set;}

        public DateTime StartDate {get; set;}
        public DateTime? EndDate {get; set;}

        [Computed]
        public double Progress {get; set;}
    }
}