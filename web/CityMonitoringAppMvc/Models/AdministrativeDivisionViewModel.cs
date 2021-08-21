using System.Collections.Generic;

namespace CityMonitoringAppMvc.Models
{
    public class AdministrativeDivisionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CoordinatesViewModel> Coordinates {get; set;}
    }

    public class CoordinatesViewModel
    {
        public double Latitude {get; set;}
        public double Longitude {get; set;}
    }
}