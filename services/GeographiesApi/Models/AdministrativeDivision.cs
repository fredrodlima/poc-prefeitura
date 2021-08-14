using NetTopologySuite.Geometries;

namespace GeographiesApi.Models
{
    public class AdministrativeDivision{
        public int Id {get; set;}
        public string Name {get; set;}
        
        public Geometry Geography { get; set; }
    }
}