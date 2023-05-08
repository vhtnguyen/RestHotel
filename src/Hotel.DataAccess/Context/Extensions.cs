using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.DataAccess.Context
{
    public static class Extensions
    {
        public static IServiceCollection AddSql(this IServiceCollection services)
        {
            // get configuration
            using var provider = services.BuildServiceProvider();
            var configuration = provider.GetService<IConfiguration>()!;

            // bind options
            var options = new SqlOptions();
            var section = configuration.GetSection("sql");
            section.Bind(options);

            // build string builder
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = options.Server,
                InitialCatalog = options.Database,
                UserID = options.Username,
                Password = options.Password,
                TrustServerCertificate= options.TrustServerCertificate,
                Encrypt= options.Encrypt,
            };

            // bind options
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.ConnectionString));
            return services;
        }
    }
}
