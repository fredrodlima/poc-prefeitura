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
    public class CitizenLocalitiesController : Controller
    {
        //Hosted web API REST Service base url
        string BaseUrl = "https://localhost:44394/";
        CitizenLocality citizenLocality = new CitizenLocality();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<CitizenLocalityViewModel>> GetAllCitizenLocalities()
        {
            List<CitizenLocalityViewModel> CitizenLocalitysViewModel = new List<CitizenLocalityViewModel>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync("api/CitizenLocalities");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var citizenLocalitysResponse = res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response received from the web api and storing into Administrative Divisions list
                    var serializer = GeoJsonSerializer.CreateDefault();
                    serializer.CheckAdditionalContent = true;
                    var stream = new MemoryStream();
                    var writer = new StreamWriter(stream);
                    writer.Write(citizenLocalitysResponse);
                    writer.Flush();
                    stream.Position = 0;

                    using (var textReader = new StreamReader(stream))
                    using (var jsonReader = new JsonTextReader(textReader))
                    {
                        foreach (var citizenLocality in serializer.Deserialize<List<CitizenLocality>>(jsonReader))
                        {
                            List<CoordinatesViewModel> coordinates = new List<CoordinatesViewModel>();
                            foreach (var geoCoordinate in citizenLocality.Location.Coordinates)
                            {
                                CoordinatesViewModel coordinate = new CoordinatesViewModel
                                {
                                    Latitude = geoCoordinate.Y,
                                    Longitude = geoCoordinate.X
                                };
                                coordinates.Add(coordinate);
                            }
                            CitizenLocalitysViewModel.Add(new CitizenLocalityViewModel
                            {
                                Id = citizenLocality.Id,
                                CitizenId = citizenLocality.CitizenId,
                                LocationTypeId = citizenLocality.LocationTypeId,
                                Name = citizenLocality.Name,
                                Coordinate = new CoordinateViewModel { Latitude = coordinates[0].Latitude, Longitude = coordinates[0].Longitude }
                            });
                        }
                    }
                }
            }
            return CitizenLocalitysViewModel;
        }

        [HttpGet]
        public async Task<CitizenLocalityViewModel> GetCitizenLocality(int citizenLocalityId)
        {
            CitizenLocalityViewModel CitizenLocalityViewModel = new CitizenLocalityViewModel();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                using var res = await client.GetAsync("api/CitizenLocalities/" + citizenLocalityId);
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var citizenLocalitysResponse = await res.Content.ReadAsStringAsync();

                    //Deserializing the response received from the web api and storing into Administrative Divisions list
                    var serializer = GeoJsonSerializer.CreateDefault();
                    serializer.CheckAdditionalContent = true;
                    var stream = new MemoryStream();
                    var writer = new StreamWriter(stream);
                    writer.Write(citizenLocalitysResponse);
                    writer.Flush();
                    stream.Position = 0;

                    using var textReader = new StreamReader(stream);
                    using var jsonReader = new JsonTextReader(textReader);
                    var citizenLocality = serializer.Deserialize<CitizenLocality>(jsonReader);

                    List<CoordinatesViewModel> coordinates = new List<CoordinatesViewModel>();
                    foreach (var geoCoordinate in citizenLocality.Location.Coordinates)
                    {
                        CoordinatesViewModel coordinate = new CoordinatesViewModel
                        {
                            Latitude = geoCoordinate.Y,
                            Longitude = geoCoordinate.X
                        };
                        coordinates.Add(coordinate);
                    }
                    CitizenLocalityViewModel = new CitizenLocalityViewModel
                    {
                        Id = citizenLocality.Id,
                        CitizenId = citizenLocality.CitizenId,
                        LocationTypeId = citizenLocality.LocationTypeId,
                        Name = citizenLocality.Name,
                        Coordinate = new CoordinateViewModel { Latitude = coordinates[0].Latitude, Longitude = coordinates[0].Longitude }
                    };
                }
            }
            return CitizenLocalityViewModel;
        }


        [HttpPost]
        public async Task<CitizenLocalityViewModel> AddCitizenLocality(CitizenLocalityViewModel citizenLocalityVM)
        {
            citizenLocality = new CitizenLocality();
            var CitizenLocalityViewModel = new CitizenLocalityViewModel();
            GeometryFactory factory = new GeometryFactory();

            Coordinate coord = new(citizenLocalityVM.Coordinate.Longitude, citizenLocalityVM.Coordinate.Latitude);
            
            citizenLocality.Location = new GeometryFactory().CreatePoint(coord);
            citizenLocality.CitizenId = citizenLocalityVM.CitizenId;
            citizenLocality.LocationTypeId = citizenLocalityVM.LocationTypeId;
            citizenLocality.Name = citizenLocalityVM.Name;           

            using (var client = new HttpClient())
            {
                string geoJson;

                var serializer = GeoJsonSerializer.Create();
                using (var stringWriter = new StringWriter())
                using (var jsonWriter = new JsonTextWriter(stringWriter))
                {
                    serializer.Serialize(jsonWriter, citizenLocality);

                    geoJson = stringWriter.ToString();

                    StringContent content = new StringContent(geoJson, Encoding.UTF8, "application/json");

                    //Passing service base url
                    client.BaseAddress = new Uri(BaseUrl);

                    //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                    using var res = await client.PostAsync("api/CitizenLocalities", content);
                    //Checking if the response is successful or not which is sent using HttpClient
                    if (res.IsSuccessStatusCode)
                    {
                        //Storing the response details received from web api
                        var citizenLocalitysResponse = await res.Content.ReadAsStringAsync();

                        serializer.CheckAdditionalContent = true;
                        var stream = new MemoryStream();
                        var writer = new StreamWriter(stream);
                        writer.Write(citizenLocalitysResponse);
                        writer.Flush();
                        stream.Position = 0;

                        using var textReader = new StreamReader(stream);
                        using var jsonReader = new JsonTextReader(textReader);
                        var citizenLocality = serializer.Deserialize<CitizenLocality>(jsonReader);


                        List<CoordinatesViewModel> coordinatesVM = new List<CoordinatesViewModel>();
                        foreach (var geoCoordinate in citizenLocality.Location.Coordinates)
                        {
                            CoordinatesViewModel coordinateVM = new CoordinatesViewModel
                            {
                                Latitude = geoCoordinate.Y,
                                Longitude = geoCoordinate.X
                            };
                            coordinatesVM.Add(coordinateVM);
                        }
                        CitizenLocalityViewModel = new CitizenLocalityViewModel
                        {
                            Id = citizenLocality.Id,
                            CitizenId = citizenLocality.CitizenId,
                            LocationTypeId = citizenLocality.LocationTypeId,
                            Name = citizenLocality.Name,
                            Coordinate = new CoordinateViewModel { Latitude = coordinatesVM[0].Latitude, Longitude = coordinatesVM[0].Longitude }
                        };
                    }
                }
            }
            return CitizenLocalityViewModel;
        }

        [HttpPut]
        public async Task<string> UpdateCitizenLocality(CitizenLocalityViewModel citizenLocalityVM)
        {
            var message = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                using var resGet = await client.GetAsync("api/CitizenLocalities/" + citizenLocalityVM.Id);
                var serializer = GeoJsonSerializer.CreateDefault();
                //Checking if the response is successful or not which is sent using HttpClient
                if (resGet.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var citizenLocalitysResponse = await resGet.Content.ReadAsStringAsync();

                    //Deserializing the response received from the web api and storing into Administrative Divisions list

                    serializer.CheckAdditionalContent = true;
                    var stream = new MemoryStream();
                    var writer = new StreamWriter(stream);
                    writer.Write(citizenLocalitysResponse);
                    writer.Flush();
                    stream.Position = 0;

                    using var textReader = new StreamReader(stream);
                    using var jsonReader = new JsonTextReader(textReader);
                    citizenLocality = serializer.Deserialize<CitizenLocality>(jsonReader);
                }

                GeometryFactory factory = new GeometryFactory();
                
                Coordinate coord = new(citizenLocalityVM.Coordinate.Longitude, citizenLocalityVM.Coordinate.Latitude);

                citizenLocality.Location = new GeometryFactory().CreatePoint(coord);
                citizenLocality.CitizenId = citizenLocalityVM.CitizenId;
                citizenLocality.LocationTypeId = citizenLocalityVM.LocationTypeId;
                citizenLocality.Name = citizenLocalityVM.Name;

                string geoJson;


                using (var stringWriter = new StringWriter())
                using (var jsonWriter = new JsonTextWriter(stringWriter))
                {
                    serializer.Serialize(jsonWriter, citizenLocality);

                    geoJson = stringWriter.ToString();

                    StringContent content = new StringContent(geoJson, Encoding.UTF8, "application/json");

                    //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                    using var res = await client.PutAsync($"api/CitizenLocalities/{citizenLocality.Id}", content);
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
        public async Task<string> DeleteCitizenLocality(int citizenLocalityId)
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
                using var res = await client.DeleteAsync("api/CitizenLocalities/" + citizenLocalityId);
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
