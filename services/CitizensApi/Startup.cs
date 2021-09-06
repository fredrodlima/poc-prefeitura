using ActiveMQ.Artemis.Client;
using ActiveMQ.Artemis.Client.Extensions.DependencyInjection;
using ActiveMQ.Artemis.Client.Extensions.Hosting;
using CitizensApi.Messaging.Producers;
using CitizensApi.Models;
using CitizensApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CitizensApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(options =>
            // {
            //     options.AddPolicy("CitizenApiPolicy",
            //         builder => builder.WithOrigins("https://localhost:5003"));
            // });

            services.AddControllers().AddJsonOptions(options => {
                // open api is currently using system.text.json
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                //options.JsonSerializerOptions.Converters.Add(new NetTopologySuite.IO.Converters.GeoJsonConverterFactory());
            });


            services.AddDbContext<CitizensDbContext>(opt => 
                 opt.UseSqlServer(Configuration.GetConnectionString("CitizensDbContext")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CitizensApi", Version = "v1" });
            });

            //Declaring the queue for requesting the tax calculation
            var endpoints = new [] { Endpoint.Create(host : "artemis", port: 5672, "admin", "Passw@rd123!") };
            services.AddActiveMq("taxcalculation-queue", endpoints)
                .AddAnonymousProducer<MessageProducer>();
            services.AddActiveMqHostedService();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CitizensApi v1"));
            }

            app.UseHttpsRedirection();

            //app.UseCors("CitizenApiPolicy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
