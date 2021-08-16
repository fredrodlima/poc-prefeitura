using CitizensApi.Services.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CitizensApi.Services
{
    public class CitizenLocalitiesService
    {
        public IEnumerable<CitizenLocalitiesModel> GetMyLocalities(int citizenId)
        {
            var citizenLocalities = new List<CitizenLocalitiesModel>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44394");
                    var url = string.Format(@"api/citizenlocalities/localities/{0}", citizenId);

                    var result = client.GetStringAsync(url).Result;

                    if (string.IsNullOrEmpty(result))
                        return citizenLocalities;

                    citizenLocalities = JsonConvert.DeserializeObject<List<CitizenLocalitiesModel>>(result);
                }
                return citizenLocalities;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return citizenLocalities;
            }
        }
    }
}