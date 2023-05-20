using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Hotel.Shared.Monitoring;

public static class Extensions
{
    public static IHostBuilder UseMonitoring(this IHostBuilder host)
    {
        host
            .ConfigureMetricsWithDefaults((context, builder) =>
            {
                var options = context.Configuration.GetOptions<MetricOptions>("metrics");
                // attach tags
                builder.Configuration.Configure(configuration =>
                {
                    options.Tags!.TryGetValue("app", out var app);
                    options.Tags!.TryGetValue("server", out var server);
                    options.Tags!.TryGetValue("env", out var env);

                    configuration.AddAppTag(string.IsNullOrEmpty(app) ? null : app);
                    configuration.AddEnvTag(string.IsNullOrEmpty(env) ? null : env);
                    configuration.AddServerTag(string.IsNullOrEmpty(server) ? null : server);
                });
                // check enable influx
                if (options.InfluxEnable)
                {
                    builder.Report.ToInfluxDb(o =>
                    {
                        o.InfluxDb.BaseUri = new Uri(options.InfluxUrl!);
                        o.InfluxDb.Database = options.Database;
                        o.InfluxDb.CreateDataBaseIfNotExists = true;
                        o.FlushInterval = TimeSpan.FromSeconds(options.Interval);
                    });
                }
            })
            .UseMetricsWebTracking()
            .UseMetrics((c, o) =>
            {
                var options = c.Configuration.GetOptions<MetricOptions>("metrics");
                if(options.PrometheusEnable)
                {
                    o.EndpointOptions = endPointOptions =>
                    {
                        endPointOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                    };
                }    
            });
        return host;
    }
    private static T GetOptions<T>(this IConfiguration configuration, string name)
    {
        var section = configuration.GetSection(name);
        var options = section.Get<T>();
        return options!;
    }
}
