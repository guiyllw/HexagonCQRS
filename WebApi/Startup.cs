using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

namespace WebApi
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSettings(services);
            ConfigurePorts(services);
            ConfigureAdapters(services);

            services.AddControllers();
            services.AddFeatureManagement();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureSettings(IServiceCollection services)
        {
            //services.Configure<MySetting>(configuration.GetSection(nameof(MySetting)));
        }

        private void ConfigurePorts(IServiceCollection services)
        {

        }

        private void ConfigureAdapters(IServiceCollection services)
        {

        }
    }
}
