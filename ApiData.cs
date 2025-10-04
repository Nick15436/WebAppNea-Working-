using System.Text.Json.Serialization;

namespace WebAppNea;

public class ApiData
{
    [JsonPropertyName("Meta Data")]
    public ApiMetadata Metadata { get; set; }

    [JsonPropertyName("Weekly Time Series")]
    public Dictionary<string, TimeSeriesDataPoint> TimeSeries { get; set; } 

}