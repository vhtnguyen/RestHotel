using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Hotel.API.HealthChecks;

public class ApiHealthCheck : IHealthCheck
{
    // ping api
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy("api endpoint is health"));
    }
}
