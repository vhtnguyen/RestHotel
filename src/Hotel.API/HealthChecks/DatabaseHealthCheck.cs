using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Hotel.API.HealthChecks;

public class DatabaseHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
