using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjectsAppMvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAppMvc.Controllers
{
    public class ProjectPhasesController : Controller
    {

        private string ProjectsApiBaseUrl;
        public ProjectPhasesController(IConfiguration configuration)
        {
            ProjectsApiBaseUrl = configuration["Services:ProjectsApiBaseUrl"];
        }

        // GET: ProjectPhases
        public async Task<IActionResult> Index()
        {
            var projectPhases = new List<ProjectPhase>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync("api/ProjectPhases");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var projectPhasesResponse = res.Content.ReadAsStringAsync().Result;

                    projectPhases = JsonConvert.DeserializeObject<List<ProjectPhase>>(projectPhasesResponse);
                }
            }
            return View(projectPhases);
        }

        // GET: ProjectPhases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectPhase = new ProjectPhase();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync($"api/ProjectPhases/{id}");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var projectPhaseResponse = res.Content.ReadAsStringAsync().Result;
                    projectPhase = JsonConvert.DeserializeObject<ProjectPhase>(projectPhaseResponse);

                    if (projectPhase == null)
                    {
                        return NotFound();
                    }
                }
            }

            return View(projectPhase);
        }

        // GET: ProjectPhases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectPhases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectId,Name,Status")] ProjectPhase projectPhase)
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
                        serializer.Serialize(jsonWriter, projectPhase);

                        string projectJson = stringWriter.ToString();

                        StringContent content = new StringContent(projectJson, Encoding.UTF8, "application/json");
                        HttpResponseMessage res = await client.PostAsync($"api/ProjectPhases/", content);
                        //Checking if the response is successful or not which is sent using HttpClient
                        if (res.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            return View(projectPhase);
        }

        // GET: ProjectPhases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectPhase = new ProjectPhase();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync($"api/ProjectPhases/{id}");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var projectPhaseResponse = res.Content.ReadAsStringAsync().Result;
                    projectPhase = JsonConvert.DeserializeObject<ProjectPhase>(projectPhaseResponse);

                    if (projectPhase == null)
                    {
                        return NotFound();
                    }
                }
            }
            return View(projectPhase);
        }

        // POST: ProjectPhases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectId,Name,Status")] ProjectPhase projectPhase)
        {
            if (id != projectPhase.Id)
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
                            serializer.Serialize(jsonWriter, projectPhase);

                            projectJson = stringWriter.ToString();

                            StringContent content = new StringContent(projectJson, Encoding.UTF8, "application/json");

                            //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                            using var res = await client.PutAsync($"api/ProjectPhases/{projectPhase.Id}", content);
                            //Checking if the response is successful or not which is sent using HttpClient
                            if (res.IsSuccessStatusCode)
                            { }
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProjectPhaseExistsAsync(projectPhase.Id))
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
            return View(projectPhase);
        }

        // GET: ProjectPhases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectPhase = new ProjectPhase();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync($"api/ProjectPhases/{id}");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var projectPhaseResponse = res.Content.ReadAsStringAsync().Result;
                    projectPhase = JsonConvert.DeserializeObject<ProjectPhase>(projectPhaseResponse);
                }
            }
            if (projectPhase == null)
            {
                return NotFound();
            }

            return View(projectPhase);
        }

        // POST: ProjectPhases/Delete/5
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
                using var res = await client.DeleteAsync("api/ProjectPhases/" + id);
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    await res.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProjectPhaseExistsAsync(int id)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ProjectsApiBaseUrl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get Administrative Divisions using HttpClient
                HttpResponseMessage res = await client.GetAsync($"api/ProjectPhases/{id}");
                //Checking if the response is successful or not which is sent using HttpClient
                if (res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var projectPhaseResponse = res.Content.ReadAsStringAsync().Result;
                    var projectPhase = JsonConvert.DeserializeObject<ProjectPhase>(projectPhaseResponse);
                    return projectPhase != null;
                }
            }
            return false;
        }
    }
}
