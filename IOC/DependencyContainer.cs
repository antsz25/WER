

using BussinessLogic.Interfaces;
using BussinessLogic.Services;
using DataLayer.Extensions;
using DataLayer.Utils;
using DomainLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Transactions;

namespace IOC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, string ConnectionString)
        {
            services.AddScoped<IEnterpriseService, EnterpriseService>();
            services.AddScoped<IPublicationService, PublicationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IJWTService, JWTService>();
            services.AddScoped<IWorkService, WorkService>();
            services.AddMainDataLayerDependencies(ConnectionString);
        }
    }
}
