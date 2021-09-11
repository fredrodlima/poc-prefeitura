using CitizensApi.Services.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CitizensApi.Services
{
    public class CitizenLocalitiesService
    {
        private string GeographiesApiBaseUrl ;
        public CitizenLocalitiesService(IConfiguration configuration)
        {
            GeographiesApiBaseUrl = configuration["Services:GeographiesApiBaseAddress"];
        }
        
        public IEnumerable<CitizenLocalitiesModel> GetMyLocalities(int citizenId)
        {
            var citizenLocalities = new List<CitizenLocalitiesModel>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GeographiesApiBaseUrl);
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