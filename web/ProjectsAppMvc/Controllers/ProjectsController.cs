using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

namespace ProjectsAppMvc.Controllers
{
    public class ProjectsController : Controller
    {
        private string ProjectsApiBaseUrl;
        public ProjectsController(IConfiguration configuration)
        {
            ProjectsApiBaseUrl = configuration["Services:ProjectsApiBaseUrl"];
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var projectVMs = new List<ProjectViewModel>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync("api/Projects");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var projectsResponse = res.Content.ReadAsStringAsync().Result;

                    var projects = JsonConvert.DeserializeObject<List<Project>>(projectsResponse);
                    projectVMs = projects.Select(p => new ProjectViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        SupervisorId = p.SupervisorId,
                        StartDate = p.StartDate,
                        EndDate = p.EndDate
                    }).ToList();
                    foreach (var project in projectVMs)
                    {
                        //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                        HttpResponseMessage res1 = await client.GetAsync($"api/IndividualMetrics/{project.Id}");
                        //Checking if the response is successful or not which is sent using HttpClient
                        if (res1.IsSuccessStatusCode)
                        {
                            //Storing the response details received from web api
                            var individualMetricPhasesResponse = res1.Content.ReadAsStringAsync().Result;

                            var individualMetric = JsonConvert.DeserializeObject<IndividualMetric>(individualMetricPhasesResponse);

                            project.Progress = individualMetric.Progress;
                        }
                    }
                }
            }
            return View(projectVMs);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project = new Project();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync($"api/Projects/{id}");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var projectResponse = res.Content.ReadAsStringAsync().Result;
                    project = JsonConvert.DeserializeObject<Project>(projectResponse);

                    if (project == null)
                    {
                        return NotFound();
                    }
                }
            }
            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SupervisorId,StartDate,EndDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                    client.DefaultRequestHeaders.Clear();

                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var serializer = JsonSerializer.Create();

                    using (var stringWriter = new StringWriter())
                    using (var jsonWriter = new JsonTextWriter(stringWriter))
                    {
                        serializer.Serialize(jsonWriter, project);

                        string projectJson = stringWriter.ToString();

                        StringContent content = new StringContent(projectJson, Encoding.UTF8, "application/json");
                        HttpResponseMessage res = await client.PostAsync($"api/Projects/", content);
                        //Checking if the response is successful or not which is sent using HttpClient
                        if (res.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }

            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project = new Project();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync($"api/Projects/{id}");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var projectResponse = res.Content.ReadAsStringAsync().Result;
                    project = JsonConvert.DeserializeObject<Project>(projectResponse);

                    if (project == null)
                    {
                        return NotFound();
                    }
                }
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SupervisorId,StartDate,EndDate")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        //Passing service base url
                        client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                        client.DefaultRequestHeaders.Clear();

                        //Define request data format
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var serializer = JsonSerializer.Create();
                        string projectJson;
                        using (var stringWriter = new StringWriter())
                        using (var jsonWriter = new JsonTextWriter(stringWriter))
                        {
                            serializer.Serialize(jsonWriter, project);

                            projectJson = stringWriter.ToString();

                            StringContent content = new StringContent(projectJson, Encoding.UTF8, "application/json");

                            //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                            using var res = await client.PutAsync($"api/Projects/{project.Id}", content);
                            //Checking if the response is successful or not which is sent using HttpClient
                            if (res.IsSuccessStatusCode)
                            { }
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProjectExistsAsync(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = new Project();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync($"api/Projects/{id}");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var projectResponse = res.Content.ReadAsStringAsync().Result;
                    project = JsonConvert.DeserializeObject<Project>(projectResponse);
                }
            }
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                using var res = await client.DeleteAsync("api/Projects/" + id);
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    await res.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProjectExistsAsync(int id)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync($"api/Projects/{id}");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var projectResponse = res.Content.ReadAsStringAsync().Result;
                    var project = JsonConvert.DeserializeObject<Project>(projectResponse);
                    return project != null;
                }
            }
            return false;
        }
    }
}
