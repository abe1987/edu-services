using edu_services.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace edu_services
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "edu_services", Version = "v1" });
            });

            services.AddScoped<Classroom>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "edu_services v1"));
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{id?}",
                defaults: new { controller = "Classroom", action = "Index" });
                endpoints.MapControllerRoute(
                name: "roster",
                pattern: "roster",
                defaults: new { controller = "Classroom", action = "GetRosterAsync" });
                endpoints.MapControllerRoute(
                    name: "teacher",
                     pattern: "teacher",
                    defaults: new { controller = "Classroom", action = "AddTeacherAsync" });
                endpoints.MapControllerRoute(
                    name: "students",
                    pattern: "students",
                     defaults: new { controller = "Classroom", action = "AddStudentsAsync" });
            });
        }
    }
}
