using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace Hotel.Shared.Logging;

public static class Extensions
{
    public static IHostBuilder UseLogging(this IHostBuilder host)
    {
        host.UseSerilog((context, service, configuration) =>
        {
            var elkOptions = context.Configuration.GetOptions<ElkOptions>("elasticSearch");
            var seqOptions = context.Configuration.GetOptions<SeqOptions>("seq");
            var seriLogOptions = context.Configuration.GetOptions<SeriLogOptions>("seriLog");

            if(!Enum.TryParse<LogEventLevel>(seriLogOptions.MinimumLevel, true, out var level))
            {
                level = LogEventLevel.Information;
            }

            configuration
                .MinimumLevel.Is(level)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);

            // config write to on configuration logger
            configurationLogging(configuration, level, seriLogOptions, seqOptions, elkOptions);
        });
            
        return host;
    }

    private static void configurationLogging(LoggerConfiguration logger, LogEventLevel level, SeriLogOptions serilog, SeqOptions seq, ElkOptions elk)
    {
        if(serilog.ElkEnable)
        {
            logger.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elk.ServerUrl!))
            {
                MinimumLogEventLevel = level,
                AutoRegisterTemplate = true,
                //AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                IndexFormat = string.IsNullOrWhiteSpace(elk.IndexFormat)
                    ? "logstash-{0:yyyy:MM:dd}"
                    : elk.IndexFormat
                // some configuration for authentication here
                
            });
        }

        if(serilog.SeqEnable)
        {
            logger.WriteTo.Seq(
                seq.ServerUrl!, 
                apiKey: seq.ApiKey);
        }

        if(serilog.ConsoleEnable)
        {
            logger.WriteTo.Console();
        }
    }

    private static T GetOptions<T>(this IConfiguration configuration, string name)
    {
        var section = configuration.GetSection(name);
        var options = section.Get<T>();
        return options!;
    }
}
