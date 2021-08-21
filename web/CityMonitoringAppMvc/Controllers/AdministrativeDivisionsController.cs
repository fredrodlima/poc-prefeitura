using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CityMonitoringAppMvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CityMonitoringAppMvc.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;

namespace CityMonitoringAppMvc.Controllers
{
    public class AdministrativeDivisionsController : Controller
    {
        //Hosted web API REST Service base url
        string BaseUrl = "https://localhost:44394/";
        private readonly ILogger<AdministrativeDivisionsController> _logger;
        private readonly GeographiesDbContext _geographiesDbContext;

        public AdministrativeDivisionsController(ILogger<AdministrativeDivisionsController> logger, GeographiesDbContext geographiesDbContext)
        {
            _logger = logger;
            _geographiesDbContext = geographiesDbContext;
        }

        public async Task<ActionResult> Index()
        {
            List<AdministrativeDivisionViewModel> administrativeDivisionsViewModel = new List<AdministrativeDivisionViewModel>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync("api/AdministrativeDivisions");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var adminDivisionsResponse = res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response received from the web api and storing into Administrative Divisions list
                    var serializer = NetTopologySuite.IO.GeoJsonSerializer.CreateDefault();
                    serializer.CheckAdditionalContent = true;
                    var stream = new MemoryStream();
                    var writer = new StreamWriter(stream);
                    writer.Write(adminDivisionsResponse);
                    writer.Flush();
                    stream.Position = 0;

                    using (var textReader = new StreamReader(stream))
                    using (var jsonReader = new JsonTextReader(textReader))
                    {
                        foreach (var adminDivision in serializer.Deserialize<List<AdministrativeDivision>>(jsonReader))
                        {
                            List<CoordinatesViewModel> coordinates = new List<CoordinatesViewModel>();
                            foreach (var geoCoordinate in adminDivision.Geography.Coordinates)
                            {
                                CoordinatesViewModel coordinate = new CoordinatesViewModel
                                {
                                    Latitude = geoCoordinate.X,
                                    Longitude = geoCoordinate.Y
                                };
                                coordinates.Add(coordinate);
                            }
                            administrativeDivisionsViewModel.Add(new AdministrativeDivisionViewModel
                            {
                                Coordinates = coordinates,
                                Id = adminDivision.Id,
                                Name = adminDivision.Name
                            });
                        }
                    }
                }
            }
            return View(administrativeDivisionsViewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
