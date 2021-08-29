using ActiveMQ.Artemis.Client;
using ActiveMQ.Artemis.Client.Extensions.DependencyInjection;
using ActiveMQ.Artemis.Client.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectsAppMvc.Messaging.Consumers;
using ProjectsAppMvc.Models.Messaging;

namespace ProjectsAppMvc
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
            services.AddControllersWithViews();

            services.AddDbContext<ProjectsDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ProjectsDbContext")));

            var endpoints = new[] { Endpoint.Create(host: "localhost", port: 5672, "admin", "admin") };
            services.AddActiveMq("watcher-projects-cluster", endpoints)
                    .AddTypedConsumer<ProjectCreated, ProjectCreatedConsumer>(RoutingType.Multicast)
                    .AddTypedConsumer<ProjectUpdated, ProjectUpdatedConsumer>(RoutingType.Multicast)
                    .AddTypedConsumer<ProjectPhaseCreated, ProjectPhaseCreatedConsumer>(RoutingType.Multicast)
                    .AddTypedConsumer<ProjectPhaseUpdated, ProjectPhaseUpdatedConsumer>(RoutingType.Multicast);

            services.AddActiveMqHostedService();

            services.AddSingleton(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
