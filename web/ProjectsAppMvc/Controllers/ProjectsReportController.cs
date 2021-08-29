using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjectsAppMvc.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProjectsAppMvc.Controllers
{
    public class ProjectsReportController : Controller
    {
        private string ProjectsApiBaseUrl;
        public ProjectsReportController(IConfiguration configuration)
        {
            ProjectsApiBaseUrl = configuration["Services:ProjectsApiBaseUrl"];
        }
        public async Task<IActionResult> Index()
        {
            var projectsReportVM = new ProjectsReportViewModel();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync("api/GlobalMetrics");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var globalMetricsResponse = res.Content.ReadAsStringAsync().Result;

                    projectsReportVM.GlobalMetrics = JsonConvert.DeserializeObject<GlobalMetric>(globalMetricsResponse);
                }

                HttpResponseMessage res1 = await client.GetAsync("api/IndividualMetrics");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res1.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var individualMetricsResponse = res1.Content.ReadAsStringAsync().Result;

                    projectsReportVM.IndividualMetrics = JsonConvert.DeserializeObject<List<IndividualMetric>>(individualMetricsResponse);
                }
            }
            return View(projectsReportVM);
        }
    }
}
