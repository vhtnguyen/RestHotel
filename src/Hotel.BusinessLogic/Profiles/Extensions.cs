using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Profiles
{
    public static class Extensions
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var assembly= Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            return services;
        }
    }
}
