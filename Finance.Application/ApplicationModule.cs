using Finance.Application.Queries.GetAllUsers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetAllUsersQuery));

            return services;
        }
    }
}
