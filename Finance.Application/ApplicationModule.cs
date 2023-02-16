using Finance.Application.Queries.User.GetAll;
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
