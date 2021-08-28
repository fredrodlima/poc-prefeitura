using CityMonitoringAppMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CityMonitoringAppMvc.Controllers
{
    public class CitizensController : Controller
    {
        //Hosted web API REST Service base url
        string BaseUrl = "https://localhost:44303/";
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<CitizenViewModel>> GetAllCitizens()
        {
            List<CitizenViewModel> citizenViewModels = new List<CitizenViewModel>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync("api/Citizens");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var citizensResponse = res.Content.ReadAsStringAsync().Result;


                    citizenViewModels = JsonConvert.DeserializeObject<List<CitizenViewModel>>(citizensResponse);
                }
            }
            return citizenViewModels;
        }

        [HttpGet]
        public async Task<List<CitizenTypeViewModel>> GetAllCitizenTypes()
        {
            List<CitizenTypeViewModel> CitizenTypeViewModels = new List<CitizenTypeViewModel>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync("api/CitizenTypes");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var citizenTypesResponse = res.Content.ReadAsStringAsync().Result;


                    CitizenTypeViewModels = JsonConvert.DeserializeObject<List<CitizenTypeViewModel>>(citizenTypesResponse);
                }
            }
            return CitizenTypeViewModels;
        }

        [HttpGet]
        public async Task<List<CitizenLocationTypeViewModel>> GetAllCitizenLocationTypes()
        {
            List<CitizenLocationTypeViewModel> CitizenLocationTypeViewModels = new List<CitizenLocationTypeViewModel>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync($"api/CitizenLocationTypes");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var citizenLocationTypesResponse = res.Content.ReadAsStringAsync().Result;


                    CitizenLocationTypeViewModels = JsonConvert.DeserializeObject<List<CitizenLocationTypeViewModel>>(citizenLocationTypesResponse);
                }
            }
            return CitizenLocationTypeViewModels;
        }

        [HttpGet]
        public async Task<List<CitizenLocationTypeViewModel>> GetCitizenLocationTypesByCitizenType(int citizenTypeId)
        {
            List<CitizenLocationTypeViewModel> CitizenLocationTypeViewModels = new List<CitizenLocationTypeViewModel>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync($"api/CitizenLocationTypes/CitizenType/{citizenTypeId}");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var citizenLocationTypesResponse = res.Content.ReadAsStringAsync().Result;


                    CitizenLocationTypeViewModels = JsonConvert.DeserializeObject<List<CitizenLocationTypeViewModel>>(citizenLocationTypesResponse);
                }
            }
            return CitizenLocationTypeViewModels;
        }
    }
}
