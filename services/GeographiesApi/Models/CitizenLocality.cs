using NetTopologySuite.Geometries;

namespace GeographiesApi.Models
{
    public class CitizenLocality
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CitizenId {get; set;}

        public int LocationTypeId {get; set;}

        public Point Location { get; set; }
    }
}
