using System.Collections.Generic;

namespace CityMonitoringAppMvc.Models
{
    public class CitizenLocalityViewModel
    {
        public int Id { get; set; }
        public int CitizenId { get; set; }
        public int LocationTypeId { get; set; }
        public CoordinateViewModel Coordinate {get; set;}

        public string Name {get; set;}

        public double Distance {get; set;}
    }

    public class CoordinateViewModel
    {
        public double Latitude {get; set;}
        public double Longitude {get; set;}
    }

    public class IndexPageViewModel
    {
        public CoordinateViewModel SearchInput {get; set;}

        public List<CitizenLocalityViewModel> CitizenLocalities {get; set;}
    }
}