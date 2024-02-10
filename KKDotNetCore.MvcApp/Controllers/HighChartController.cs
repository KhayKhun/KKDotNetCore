using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KKDotNetCore.MvcApp.Controllers
{
    public class HighChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult SemiCircle()
        {
            var data = new SemiCircleModel()
            {
                Title = "Components of atmosphere",
                Name = "Concentration",
                Data = new List<List<object>>()
                {
                    new List<object>()
                    {
                        "Nitrogen",78
                    },new List<object>()
                    {
                        "Oxygen",20.9
                    },new List<object>()
                    {
                        "Other",0.17
                    },new List<object>()
                    {
                        "Argon",0.90
                    },new List<object>()
                    {
                        "Carbon Dioxide",0.03
                    },
                }
            };
            return View(data);
        }

        public async Task<IActionResult> TimeSeries()
        {
            var data = new TimeSeriesChartModel()
            {
                Title = "USD to EUR exchange rate over time",
                TitleX = "",
                TitleY = "Exchange rate'",
                Data = { }
            };
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://cdn.jsdelivr.net/gh/highcharts/highcharts@v10.3.3/samples/data/usdeur.json");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic jsonData = JsonConvert.DeserializeObject(responseBody);
                    // Here, you can parse the JSON response as needed

                    data.Data = jsonData;

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"HttpRequestException: {e.Message}");
                }
            }
            return View(data);
        }
    }
}
