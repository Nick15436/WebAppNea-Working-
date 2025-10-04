using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppNea.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly HttpClient _httpClient;

    public IndexModel(ILogger<IndexModel> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public void OnGet()
    {
        try
        {
            
            // Open the text file using a stream reader.
            using StreamReader reader = new("API_KEY");
            
            // Read the stream as a string.
            string apiKey = reader.ReadToEnd();

            
            string url = $"https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&symbol=AAPL&apikey={apiKey}";

           

            ApiData response = _httpClient
                .GetFromJsonAsync<ApiData>(url)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            
             //_logger.LogInformation(response.TimeSeries["2024-06-02"].Open);

            
            
            List<TimeSeriesDataPoint> timeSeriesList = new List<TimeSeriesDataPoint>();

            foreach (TimeSeriesDataPoint dataPoint in response.TimeSeries.Values)
            {
                timeSeriesList.Add(dataPoint);
            }

            for (int i = 1; i < 6; i++)
            {
                _logger.LogInformation("DataPoint " + i);
                timeSeriesList[i].PrintValues();
                _logger.LogInformation("");
            }

            _logger.LogInformation($"The next price will be: {timeSeriesList[5].Close}");
            
            
            

        }
        //the catch only works for errors in which the file could not be read
        catch (IOException e) 
        {
            _logger.LogInformation("The file could not be read:");
            _logger.LogInformation(e.Message);
        }
        
        
        
        
    }
}