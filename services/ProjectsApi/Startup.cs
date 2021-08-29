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
                    builder => builder.WithOrigins("https://localhost:44305"));
            });

            services.AddControllers();

            services.AddDbContext<ProjectsDbContext>(opt => 
                    opt.UseSqlServer(@"Data Source=DESKTOP-MLSTEDC;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False; Database=projects-db"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectsApi", Version = "v1" });
            });

            //Declaring the queue for project and project phase updates
            var endpoints = new[] { Endpoint.Create(host: "localhost", port: 5672, "admin", "admin") };
            services.AddActiveMq("watcher-projects-cluster", endpoints)
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

            app.UseHttpsRedirection();

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
