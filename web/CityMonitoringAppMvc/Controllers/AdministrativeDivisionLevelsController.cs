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
    public class AdministrativeDivisionLevelsController : Controller
    {
        //Hosted web API REST Service base url
        string BaseUrl = "https://localhost:44394/";
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<AdministrativeDivisionLevelViewModel>> GetAllAdministrativeDivisionLevels()
        {
            List<AdministrativeDivisionLevelViewModel> administrativeDivisionLevelsViewModel = new List<AdministrativeDivisionLevelViewModel>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync("api/AdministrativeDivisionLevels");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var adminDivisionLevelsReponse = res.Content.ReadAsStringAsync().Result;


                    administrativeDivisionLevelsViewModel = JsonConvert.DeserializeObject<List<AdministrativeDivisionLevelViewModel>>(adminDivisionLevelsReponse);
                }
            }
            return administrativeDivisionLevelsViewModel;
        }
    }
}
