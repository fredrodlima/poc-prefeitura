using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CityMonitoringAppMvc.Models;
using NetTopologySuite.Geometries;

namespace CityMonitoringAppMvc.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly GeographiesDbContext _geographiesDbContext;

        public HomeController(ILogger<HomeController> logger, GeographiesDbContext geographiesDbContext)
        {
            _logger = logger;
            _geographiesDbContext = geographiesDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search([FromForm] IndexPageViewModel indexModel)
        {
            var indexViewModel = new IndexPageViewModel
            {
                SearchInput = indexModel.SearchInput
            };

            //Convert the input latitude and longitude to a Point
            var location = new Point(indexModel.SearchInput.Latitude, indexModel.SearchInput.Longitude) { SRID = 4326 };

            //Fetch the citizen localities and their distances from the input location using spatial queries
            var citizenLocalities = _geographiesDbContext.CitizenLocalities.Select(cl => new { Place = cl, Distance = cl.Location.Distance(location) }).ToList();

            //Ordering the result in the ascending order of distance
            indexViewModel.CitizenLocalities = citizenLocalities
            .OrderBy(x => x.Distance)
            .Select(cl => new CitizenLocalityViewModel
            {
                Distance = Math.Round(cl.Distance, 6),
                Latitude = cl.Place.Location.X,
                Longitude = cl.Place.Location.Y,
                Name = cl.Place.Name
            }).ToList();

            return View("Index", indexViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
