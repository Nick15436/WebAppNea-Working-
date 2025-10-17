using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppNea.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly HttpClient _httpClient;
    public string CandlesData;
    public string LabelsData;

    public IndexModel(ILogger<IndexModel> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public void OnGet(string ticker)
    {
        try
        {
            
            
            // Open the text file using a stream reader.
            using StreamReader reader = new("API_KEY");
            
            // Read the stream as a string.
            string apiKey = reader.ReadToEnd();

            
            string url = $"https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&symbol={ticker}&apikey={apiKey}";

           

            HttpResponseMessage response = _httpClient
                .GetAsync(url)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

           string responseContent = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
           _logger.LogInformation(responseContent);

           ApiData apiData = JsonSerializer.Deserialize<ApiData>(responseContent);
            
            List<TimeSeriesDataPoint> timeSeriesList = new List<TimeSeriesDataPoint>();
            List<string> dates = new List<string>();

            foreach (TimeSeriesDataPoint dataPoint in apiData.TimeSeries.Values)
            {
                timeSeriesList.Add(dataPoint);
            }

            foreach (string key in apiData.TimeSeries.Keys)
            {
                dates.Add(key);
            }

            timeSeriesList.Reverse();
            dates.Reverse();
            
            CandlesData = "[" + timeSeriesList[0].ToString();
            LabelsData = "[" + $"'{dates[0]}'";
            for (int i = 1; i < timeSeriesList.Count; i++)
            {
                _logger.LogInformation("DataPoint " + i);
               _logger.LogInformation(timeSeriesList[i].ToString()); 
                _logger.LogInformation("");
                
                
                CandlesData = CandlesData + ", " + timeSeriesList[i].ToString();
                LabelsData = LabelsData + ", " + $"'{dates[i]}'";

            }

            LabelsData = LabelsData + "]";
            CandlesData = CandlesData + "]";
            
            _logger.LogInformation($"The next price will be: {timeSeriesList[5].Close}");
            /*
             The C# part of the program interacts with the .cshtml files as if the html and javascript in that file is
             one big string.
             
            Example:
            CandlesData = "[[20, 34, 10, 38],[40, 35, 30, 50],[31, 38, 33, 44],[38, 15, 5, 42]]";
            LabelsData = "['2017-10-24', '2017-10-25', '2017-10-26', '2017-10-27']";
            */
            
            // order is: close, open, low, high
            //CandlesData = "[[20, 34, 10, 38],[40, 35, 30, 50],[31, 38, 33, 44],[38, 15, 5, 42]]";
            //LabelsData = "['2017-10-24', '2017-10-25', '2017-10-26', '2017-10-27', '2017-10-24', '2017-10-25', '2017-10-26', '2017-10-27']";
            
        }
        //the catch only works for errors in which the file could not be read
        catch (IOException e) 
        {
            _logger.LogInformation("The file could not be read:");
            _logger.LogInformation(e.Message);
        }
        
       
        
        
    }
}