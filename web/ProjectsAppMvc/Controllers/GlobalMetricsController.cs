using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjectsAppMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProjectsAppMvc.Controllers
{
    public class GlobalMetricsController : Controller
    {
        //Hosted web API REST Service base url
        private string ProjectsApiBaseUrl;
        public GlobalMetricsController(IConfiguration configuration)
        {
            ProjectsApiBaseUrl = configuration["Services:ProjectsApiBaseUrl"];
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<GlobalMetricViewModel> GetGlobalMetric()
        {
            var globalMetricsViewModel = new GlobalMetricViewModel();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get GlobalMetrics using HttpClient
                HttpResponseMessage res = await client.GetAsync("api/GlobalMetrics");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var globalMetricsReponse = res.Content.ReadAsStringAsync().Result;


                    globalMetricsViewModel = JsonConvert.DeserializeObject<GlobalMetricViewModel>(globalMetricsReponse);
                }
            }
            return globalMetricsViewModel;
        }
    }
}
