using System.Text.Json.Serialization;

namespace WebAppNea;

public class TickerSearchResponse
{
    [JsonPropertyName("bestMatches")]
    public List<TickerMatch> BestMatches { get; set; }
}

public class TickerMatch
{
    [JsonPropertyName("1. symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("2. name")]
    public string Name { get; set; }

    [JsonPropertyName("3. type")]
    public string Type { get; set; }

    [JsonPropertyName("4. region")]
    public string Region { get; set; }
}