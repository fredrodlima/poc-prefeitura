using CityMonitoringAppMvc.Entities;
using CityMonitoringAppMvc.Models;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CityMonitoringAppMvc.Controllers
{
    public class AdministrativeDivisionsController : Controller
    {
        //Hosted web API REST Service base url
        string BaseUrl = "http://geographies-api";
        AdministrativeDivision adminDivision = new AdministrativeDivision();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<AdministrativeDivision>> GetAllAdministrativeDivisionModels()
        {
            List<AdministrativeDivision> administrativeDivisions = new List<AdministrativeDivision>();
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
                    var serializer = GeoJsonSerializer.CreateDefault();
                    serializer.CheckAdditionalContent = true;
                    var stream = new MemoryStream();
                    var writer = new StreamWriter(stream);
                    writer.Write(adminDivisionsResponse);
                    writer.Flush();
                    stream.Position = 0;

                    using (var textReader = new StreamReader(stream))
                    using (var jsonReader = new JsonTextReader(textReader))
                    {
                        administrativeDivisions = serializer.Deserialize<List<AdministrativeDivision>>(jsonReader);
                    }
                }
            }
            return administrativeDivisions;
        }

        [HttpGet]
        public async Task<List<AdministrativeDivisionViewModel>> GetAllAdministrativeDivisions()
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
                    var serializer = GeoJsonSerializer.CreateDefault();
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
                                    Latitude = geoCoordinate.Y,
                                    Longitude = geoCoordinate.X
                                };
                                coordinates.Add(coordinate);
                            }
                            administrativeDivisionsViewModel.Add(new AdministrativeDivisionViewModel
                            {
                                Coordinates = coordinates,
                                Id = adminDivision.Id,
                                AdministrativeDivisionLevelId = adminDivision.AdministrativeDivisionLevelId,
                                Name = adminDivision.Name
                            });
                        }
                    }
                }
            }
            return administrativeDivisionsViewModel;
        }

        [HttpGet]
        public async Task<AdministrativeDivisionViewModel> GetAdministrativeDivision(int adminDivisionId)
        {
            AdministrativeDivisionViewModel administrativeDivisionViewModel = new AdministrativeDivisionViewModel();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                using var res = await client.GetAsync("api/AdministrativeDivisions/" + adminDivisionId);
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var adminDivisionsResponse = await res.Content.ReadAsStringAsync();

                    //Deserializing the response received from the web api and storing into Administrative Divisions list
                    var serializer = GeoJsonSerializer.CreateDefault();
                    serializer.CheckAdditionalContent = true;
                    var stream = new MemoryStream();
                    var writer = new StreamWriter(stream);
                    writer.Write(adminDivisionsResponse);
                    writer.Flush();
                    stream.Position = 0;

                    using var textReader = new StreamReader(stream);
                    using var jsonReader = new JsonTextReader(textReader);
                    var adminDivision = serializer.Deserialize<AdministrativeDivision>(jsonReader);

                    List<CoordinatesViewModel> coordinates = new List<CoordinatesViewModel>();
                    foreach (var geoCoordinate in adminDivision.Geography.Coordinates)
                    {
                        CoordinatesViewModel coordinate = new CoordinatesViewModel
                        {
                            Latitude = geoCoordinate.Y,
                            Longitude = geoCoordinate.X
                        };
                        coordinates.Add(coordinate);
                    }
                    administrativeDivisionViewModel = new AdministrativeDivisionViewModel
                    {
                        Coordinates = coordinates,
                        Id = adminDivision.Id,
                        AdministrativeDivisionLevelId = adminDivision.AdministrativeDivisionLevelId,
                        Name = adminDivision.Name
                    };
                }
            }
            return administrativeDivisionViewModel;
        }


        [HttpPost]
        public async Task<AdministrativeDivisionViewModel> AddAdministrativeDivision(AdministrativeDivisionViewModel adminDivisionVM)
        {
            adminDivision = new AdministrativeDivision();
            var administrativeDivisionViewModel = new AdministrativeDivisionViewModel();
            List<Coordinate> coordinates = new();

            foreach (var coordinate in adminDivisionVM.Coordinates)
            {
                Coordinate coord = new(coordinate.Longitude, coordinate.Latitude);
                coordinates.Add(coord);
            }
            var g1 = new GeometryFactory().CreateLinearRing(coordinates.ToArray());
            var holes = Array.Empty<LinearRing>();
            adminDivision.Geography = new GeometryFactory().CreatePolygon(g1, holes);
            adminDivision.Name = adminDivisionVM.Name;
            adminDivision.AdministrativeDivisionLevelId = adminDivisionVM.AdministrativeDivisionLevelId;

            using (var client = new HttpClient())
            {
                string geoJson;

                var serializer = GeoJsonSerializer.Create();
                using (var stringWriter = new StringWriter())
                using (var jsonWriter = new JsonTextWriter(stringWriter))
                {
                    serializer.Serialize(jsonWriter, adminDivision);

                    geoJson = stringWriter.ToString();

                    StringContent content = new StringContent(geoJson, Encoding.UTF8, "application/json");

                    //Passing service base url
                    client.BaseAddress = new Uri(BaseUrl);

                    //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                    using var res = await client.PostAsync("api/AdministrativeDivisions", content);
                    //Checking if the response is successful or not which is sent using HttpClient
                    if (res.IsSuccessStatusCode)
                    {
                        //Storing the response details received from web api
                        var adminDivisionsResponse = await res.Content.ReadAsStringAsync();

                        serializer.CheckAdditionalContent = true;
                        var stream = new MemoryStream();
                        var writer = new StreamWriter(stream);
                        writer.Write(adminDivisionsResponse);
                        writer.Flush();
                        stream.Position = 0;

                        using var textReader = new StreamReader(stream);
                        using var jsonReader = new JsonTextReader(textReader);
                        var adminDivision = serializer.Deserialize<AdministrativeDivision>(jsonReader);


                        List<CoordinatesViewModel> coordinatesVM = new List<CoordinatesViewModel>();
                        foreach (var geoCoordinate in adminDivision.Geography.Coordinates)
                        {
                            CoordinatesViewModel coordinateVM = new CoordinatesViewModel
                            {
                                Latitude = geoCoordinate.Y,
                                Longitude = geoCoordinate.X
                            };
                            coordinatesVM.Add(coordinateVM);
                        }
                        administrativeDivisionViewModel = new AdministrativeDivisionViewModel
                        {
                            Coordinates = coordinatesVM,
                            Id = adminDivision.Id,
                            AdministrativeDivisionLevelId = adminDivision.AdministrativeDivisionLevelId,
                            Name = adminDivision.Name
                        };
                    }
                }
            }
            return administrativeDivisionViewModel;
        }

        [HttpPut]
        public async Task<string> UpdateAdministrativeDivision(AdministrativeDivisionViewModel adminDivisionVM)
        {
            var message = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                using var resGet = await client.GetAsync("api/AdministrativeDivisions/" + adminDivisionVM.Id);
                var serializer = GeoJsonSerializer.CreateDefault();
                //Checking if the response is successful or not which is sent using HttpClient
                if (resGet.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var adminDivisionsResponse = await resGet.Content.ReadAsStringAsync();

                    //Deserializing the response received from the web api and storing into Administrative Divisions list

                    serializer.CheckAdditionalContent = true;
                    var stream = new MemoryStream();
                    var writer = new StreamWriter(stream);
                    writer.Write(adminDivisionsResponse);
                    writer.Flush();
                    stream.Position = 0;

                    using var textReader = new StreamReader(stream);
                    using var jsonReader = new JsonTextReader(textReader);
                    adminDivision = serializer.Deserialize<AdministrativeDivision>(jsonReader);
                }

                GeometryFactory factory = new GeometryFactory();
                List<Coordinate> coordinates = new();

                foreach (var coordinate in adminDivisionVM.Coordinates)
                {
                    Coordinate coord = new(coordinate.Longitude, coordinate.Latitude);
                    coordinates.Add(coord);
                }
                var g1 = new GeometryFactory().CreateLinearRing(coordinates.ToArray());
                var holes = Array.Empty<LinearRing>();

                adminDivision.Geography = new GeometryFactory().CreatePolygon(g1, holes);
                adminDivision.Name = adminDivisionVM.Name;
                adminDivision.AdministrativeDivisionLevelId = adminDivisionVM.AdministrativeDivisionLevelId;


                string geoJson;


                using (var stringWriter = new StringWriter())
                using (var jsonWriter = new JsonTextWriter(stringWriter))
                {
                    serializer.Serialize(jsonWriter, adminDivision);

                    geoJson = stringWriter.ToString();

                    StringContent content = new StringContent(geoJson, Encoding.UTF8, "application/json");

                    //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                    using var res = await client.PutAsync($"api/AdministrativeDivisions/{adminDivision.Id}", content);
                    //Checking if the response is successful or not which is sent using HttpClient
                    if (res.IsSuccessStatusCode)
                    {
                        //Storing the response details received from web api
                        message = await res.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        message = $"Um erro ocorreu ao atualizar a divisão administrativa! Status: {res.StatusCode}";
                    }
                }
            }

            return message;
        }

        [HttpDelete]
        public async Task<string> DeleteAdministrativeDivision(int adminDivisionId)
        {
            string message = "";
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                using var res = await client.DeleteAsync("api/AdministrativeDivisions/" + adminDivisionId);
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    message = await res.Content.ReadAsStringAsync();
                }
            }
            return message;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
