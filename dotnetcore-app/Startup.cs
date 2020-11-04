using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MvcApp.Models;
using Prometheus;

using Datadog.Trace.OpenTracing;
using OpenTracing.Util;
using OpenTracing;

namespace MvcApp
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
            // Create an OpenTracing ITracer with the default setting
            OpenTracing.ITracer tracer = OpenTracingTracerFactory.CreateTracer();

            // Use the tracer with ASP.NET Core dependency injection
            services.AddSingleton<ITracer>(tracer);

            // Use the tracer with OpenTracing.GlobalTracer.Instance
            GlobalTracer.Register(tracer);

            services.AddControllersWithViews();

            services.AddDbContext<ProductsContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ProductsContext")));
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Enable exporting Prometheus metrics
            app.UseHttpMetrics();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Exporting Prometheus metrics to /metrics
                endpoints.MapMetrics("/metrics");
            });
        }
    }
}
