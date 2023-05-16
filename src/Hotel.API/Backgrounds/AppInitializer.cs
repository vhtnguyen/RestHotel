using Microsoft.EntityFrameworkCore;

namespace Hotel.API.Backgrounds
{
    public class AppInitializer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public AppInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(a => a.GetTypes())
               .Where(a => typeof(DbContext).IsAssignableFrom(a) && !a.IsInterface && a != typeof(DbContext));

            using var scope = _serviceProvider.CreateScope();
            foreach (var dbContextType in dbContextTypes)
            {
                var dbContext = scope.ServiceProvider.GetRequiredService(dbContextType) as DbContext;
                if (dbContext is null)
                {
                    continue;
                }

                await dbContext.Database.MigrateAsync();
            }
        }
    }
}

/*
    Cd vào folder Hotel.DataAccess: dotnet-ef migrations add Initial --context AppDbContext --startup-project ../Hotel.API -o Migrations
                                    dotnet database update
*/