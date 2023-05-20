namespace Hotel.Shared.Monitoring;

public class MetricOptions
{
    public bool PrometheusEnable { get; set; }

    // for influxdb
    public bool InfluxEnable { get; set; }
    public string? InfluxUrl { get; set; }
    public int Interval { get; set; }
    public string? Database { get; set; }
    // tag
    public Dictionary<string, string>? Tags { get;set; } 
}
