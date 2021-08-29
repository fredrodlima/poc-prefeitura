using System.Collections.Generic;

namespace ProjectsAppMvc.Models
{
    public class ProjectsReportViewModel
    {
        public GlobalMetric GlobalMetrics {get; set;}
        public ICollection<IndividualMetric> IndividualMetrics { get; set; }
    }
}
