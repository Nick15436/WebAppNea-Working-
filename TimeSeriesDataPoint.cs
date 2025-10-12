using System.Text.Json.Serialization;
using System;

namespace WebAppNea;

public class TimeSeriesDataPoint
{
    //Each JSON datapoint from the API response has all of these properties. This is a way to get these properties for each individual datapoint.
    [JsonPropertyName("1. open")]
    public float Open { get; set; }
    
    [JsonPropertyName("2. high")]
    public float High { get; set; }
    
    [JsonPropertyName("3. low")]
    public float Low { get; set; }
    
    [JsonPropertyName("4. close")]
    public float Close { get; set; }
    
    [JsonPropertyName("5. volume")]
    public float Volume { get; set; }


    public override string ToString()
    {
        return $"[{Close}, {Open}, {Low}, {High}]";
    }


    
}