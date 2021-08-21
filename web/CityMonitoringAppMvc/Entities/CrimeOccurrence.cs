using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;
namespace CityMonitoringAppMvc.Entities
{
    public class CrimeOccurrence
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PoliceDepartmentId { get; set; }
        public int PoliceReportId { get; set; }
        public int CrimeTypeId { get; set; }

        [Column(TypeName = "geography")]
        public Point Location { get; set; }
    }
}