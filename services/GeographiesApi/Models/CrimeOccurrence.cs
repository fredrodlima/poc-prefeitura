using NetTopologySuite.Geometries;

namespace GeographiesApi.Models
{
    public class CrimeOccurrence
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int PoliceDepartmentId { get; set; }

        public int PoliceReportId { get; set; }
        public int CrimeTypeId { get; set; }

        public Point Location { get; set; }
    }
}
