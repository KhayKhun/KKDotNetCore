using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KKDotNetCore.MvcApp.Controllers
{
    public class CanvasJsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Pyramid()
        {
            var data = new PyramidModel()
            {
                Title = "OKNRSR",
                DataPoints = new List<DataPointsModel>()
                {
                    new DataPointsModel()
                    {
                        y=26.66,
                        label="Sleep"
                    },new DataPointsModel()
                    {
                        y=16,
                        label="Eat"
                    },new DataPointsModel()
                    {
                        y=57.34,
                        label="Code"
                    }
                }
            };
            return View(data);
		}
		public IActionResult RangeArea()
		{
            var data = new RangeAreaModel()
            {
                Title = "Temperature in Toronto - Jan 2024",
                TitleY = "Temperature (°C)",
                SuffixY = "°C",
                TitleX = "",
                DataPoints = new List<RangeAreaDataPoint>() {
                    new RangeAreaDataPoint() { x = new DateTime(2024, 01, 01), y = new List<int>() { 15, 21 } },
                    new RangeAreaDataPoint() { x = new DateTime(2024, 01, 02), y = new List<int>() { 13, 27 } },
                    new RangeAreaDataPoint() { x = new DateTime(2024, 01, 03), y = new List<int>() { 14, 23 } },
                    new RangeAreaDataPoint() { x = new DateTime(2024, 01, 04), y = new List<int>() { 17, 25 } },
                    new RangeAreaDataPoint() { x = new DateTime(2024, 01, 05), y = new List<int>() { 16, 23 } },
                    new RangeAreaDataPoint() { x = new DateTime(2024, 01, 06), y = new List<int>() { 16, 29 } },
                    new RangeAreaDataPoint() { x = new DateTime(2024, 01, 07), y = new List<int>() { 18, 27 } },
                    new RangeAreaDataPoint() { x = new DateTime(2024, 01, 08), y = new List<int>() { 16, 25 } },
                    new RangeAreaDataPoint() { x = new DateTime(2024, 01, 09), y = new List<int>() { 15, 25 } },
                    new RangeAreaDataPoint() { x = new DateTime(2024, 01, 10), y = new List<int>() { 16, 23 } },
                    new RangeAreaDataPoint() { x = new DateTime(2024, 01, 11), y = new List<int>() { 15, 26 } }
                }

            };
            return View(data);
		}
	}
}
