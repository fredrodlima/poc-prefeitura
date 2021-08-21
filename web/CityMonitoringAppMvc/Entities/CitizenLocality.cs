using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;
namespace CityMonitoringAppMvc.Entities
{
    public class CitizenLocality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CitizenId { get; set; }
        public int LocationTypeId { get; set; }

        [Column(TypeName = "geography")]
        public Point Location { get; set; }
    }
}