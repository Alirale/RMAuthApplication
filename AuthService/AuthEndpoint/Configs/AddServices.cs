using Application.Services;
using Common.RepositoryInterfaces;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace AuthEndpoint.Configs
{
    public static class AddServices
    {
        public static void AddRMServices(this IServiceCollection services)
        {
            services.AddScoped<IUserAccessRepository, UserAccessRepository>();
            services.AddScoped<ITokenGenerationService, TokenGenerationService>();
        }
    }
}
