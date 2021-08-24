using NetTopologySuite.Geometries;

namespace CityMonitoringAppMvc.Entities
{
    public class AdministrativeDivision
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AdministrativeDivisionLevelId { get; set; }
        public Geometry Geography { get; set; }
    }
}