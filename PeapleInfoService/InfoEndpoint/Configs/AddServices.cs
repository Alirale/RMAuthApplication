using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace AuthEndpoint.Configs
{
    public static class AddServices
    {
        public static void AddRMServices(this IServiceCollection services)
        {
            services.AddScoped<IUserInfosRepository, UserInfosRepository>();
            services.AddHttpClient();
        }
    }
}
