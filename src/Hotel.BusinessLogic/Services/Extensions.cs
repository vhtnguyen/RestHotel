using Hotel.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Services
{
   public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // add normally\
  
            services.AddScoped<IRoomRegulationService, RoomRegulationService>();
            return services;
        }
    }
}
