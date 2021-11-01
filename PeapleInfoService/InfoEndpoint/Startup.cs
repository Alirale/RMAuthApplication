using AuthEndpoint.Configs;
using Common.DTOs;
using InfoEndpoint.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InfoEndpoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private TokenModel tokenSettings { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            tokenSettings = Configuration.GetSection("JWtConfig").Get<TokenModel>();

            services.AddRMServices();

            services.AddControllers();

            services.AddJwtAuthorization(tokenSettings);

            services.AddSwaggerService();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwaggerService(env);
            }

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
