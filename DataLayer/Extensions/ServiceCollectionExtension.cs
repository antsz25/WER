using DataLayer.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Arch.EntityFrameworkCore.UnitOfWork;
using DomainLayer.Interfaces;
using DataLayer.Repositories;

namespace DataLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMainDataLayerDependencies(this IServiceCollection services, string connectionString)
        {
            services
                    .AddDbContext<MainContext>(opt => opt.UseSqlServer(connectionString))
                    .AddUnitOfWork<MainContext>()
                    .AddScoped<IEnterpriseRepository, EnterpriseRepository>()
                    .AddScoped<IPublicationRepository, PublicationRepository>()
                    .AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<IWorkRepository, WorkRepository>();
            return services;
        }
    }
}
