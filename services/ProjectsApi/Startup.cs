using ActiveMQ.Artemis.Client;
using ActiveMQ.Artemis.Client.Extensions.DependencyInjection;
using ActiveMQ.Artemis.Client.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProjectsApi.Messaging.Producers;
using ProjectsApi.Models;

namespace ProjectsApi
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
            services.AddCors(options =>
            {
                options.AddPolicy("ProjectsApiPolicy",
                    builder => builder.WithOrigins("http://projects-app-mvc"));
            });

            services.AddControllers();

            services.AddDbContext<ProjectsDbContext>(opt => 
                    opt.UseSqlServer(Configuration.GetConnectionString("ProjectsDbContext")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectsApi", Version = "v1" });
            });

            //Declaring the queue for project and project phase updates
            var endpoints = new[] { Endpoint.Create(host: "artemis", port: 5672, "admin", "Passw@rd123!") };
            services.AddActiveMq("watcher-projects", endpoints)
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectsApi v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("ProjectsApiPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
