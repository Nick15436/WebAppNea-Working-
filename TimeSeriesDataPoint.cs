using System.Text.Json.Serialization;
using System;

namespace WebAppNea;

public class TimeSeriesDataPoint
{
    //Each JSON datapoint from the API response has all of these properties. This is a way to get these properties for each individual datapoint.
    [JsonPropertyName("1. open")]
    public string Open { get; set; }
    
    [JsonPropertyName("2. high")]
    public string High { get; set; }
    
    [JsonPropertyName("3. low")]
    public string Low { get; set; }
    
    [JsonPropertyName("4. close")]
    public string Close { get; set; }
    
    [JsonPropertyName("5. volume")]
    public string Volume { get; set; }

    
    //Converts datapoint to string and also puts it in correct format.
    public override string ToString()
    {
        return $"[{Close}, {Open}, {Low}, {High}]";
    }


    
}