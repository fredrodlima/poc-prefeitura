using System.Collections.Generic;

namespace CityMonitoringAppMvc.Models
{
    public class CitizenLocalityViewModel
    {
        public double Latitude {get; set;}
        public double Longitude {get; set;}

        public string Name {get; set;}

        public double Distance {get; set;}
    }

    public class SearchInputModel
    {
        public double Latitude {get; set;}
        public double Longitude {get; set;}
    }

    public class IndexPageViewModel
    {
        public SearchInputModel SearchInput {get; set;}

        public List<CitizenLocalityViewModel> CitizenLocalities {get; set;}
    }
}