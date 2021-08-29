using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjectsAppMvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAppMvc.Messaging
{
    public class ConsumerHelpers
    {
        private string ProjectsApiBaseUrl;
        public ConsumerHelpers(IConfiguration configuration)
        {
            ProjectsApiBaseUrl = configuration["Services:ProjectsApiBaseUrl"];
        }
        public async Task Consume(int projectId)
        {
            using (var client = new HttpClient())
            {
                var individualMetric = new IndividualMetric();
                var globalMetric = new GlobalMetric();

                //Passing service base url
                client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync($"api/Projects/{projectId}");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var projectResponse = res.Content.ReadAsStringAsync().Result;

                    var project = JsonConvert.DeserializeObject<Project>(projectResponse);
                    //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                    HttpResponseMessage res1 = await client.GetAsync("api/ProjectPhases");
                    //Checking if the response is successful or not which is sent using HttpClient
                    if (res1.IsSuccessStatusCode)
                    {
                        //Storing the response details received from web api
                        var projectPhasesResponse = res1.Content.ReadAsStringAsync().Result;

                        var projectPhases = JsonConvert.DeserializeObject<List<ProjectPhase>>(projectPhasesResponse);

                        //Handle changes to global metrics
                        globalMetric.Id = 1;
                        globalMetric.PhasesNotStarted = projectPhases.Count(p => p.Status == PhaseStatus.NotStarted);
                        globalMetric.PhasesInProgress = projectPhases.Count(p => p.Status == PhaseStatus.InProgress);
                        globalMetric.PhasesCompleted = projectPhases.Count(p => p.Status == PhaseStatus.Completed);
                        globalMetric.Progress = CalculationHelpers.CalculateProgress(projectPhases);

                        var serializer = JsonSerializer.Create();

                        using (var stringWriter = new StringWriter())
                        using (var jsonWriter = new JsonTextWriter(stringWriter))
                        {
                            serializer.Serialize(jsonWriter, globalMetric);

                            string globalMetricJson = stringWriter.ToString();

                            StringContent content = new StringContent(globalMetricJson, Encoding.UTF8, "application/json");
                            var putResponse = await client.PutAsync("api/GlobalMetrics", content).ConfigureAwait(false);
                        }

                        using (var stringWriter = new StringWriter())
                        using (var jsonWriter = new JsonTextWriter(stringWriter))
                        {
                            var res2 = await client.GetAsync($"api/IndividualMetrics/{project.Id}");
                           
                            //Handle changes for individual metrics
                            projectPhases = projectPhases.Where(p => p.ProjectId == project.Id).ToList();

                            individualMetric.ProjectId = project.Id;
                            individualMetric.PhasesNotStarted = projectPhases.Count(p => p.Status == PhaseStatus.NotStarted);
                            individualMetric.PhasesInProgress = projectPhases.Count(p => p.Status == PhaseStatus.InProgress);
                            individualMetric.PhasesCompleted = projectPhases.Count(p => p.Status == PhaseStatus.Completed);
                            individualMetric.Progress = CalculationHelpers.CalculateProgress(projectPhases);
                           
                            if (res2.IsSuccessStatusCode)
                            {
                                var individualMetricResponse = res2.Content.ReadAsStringAsync().Result;
                                var individualMetricDb = JsonConvert.DeserializeObject<IndividualMetric>(individualMetricResponse);
                                individualMetric.Id = individualMetricDb.Id;

                                serializer.Serialize(jsonWriter, individualMetric);

                                string individualMetricJson = stringWriter.ToString();

                                StringContent content = new StringContent(individualMetricJson, Encoding.UTF8, "application/json");

                                await client.PutAsync($"api/IndividualMetrics/{project.Id}", content).ConfigureAwait(false);
                            }
                            else
                            {
                                serializer.Serialize(jsonWriter, individualMetric);

                                string individualMetricJson = stringWriter.ToString();

                                StringContent content = new StringContent(individualMetricJson, Encoding.UTF8, "application/json");
                                await client.PostAsync($"api/IndividualMetrics/", content).ConfigureAwait(false);
                            }
                        }
                    }
                    else
                    {
                        individualMetric.ProjectId = project.Id;
                        individualMetric.PhasesNotStarted = 0;
                        individualMetric.PhasesInProgress = 0;
                        individualMetric.PhasesCompleted = 0;
                        individualMetric.Progress = 0;
                    }

                }
            }
        }
    }
}
