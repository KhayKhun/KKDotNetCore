using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KKDotNetCore.MvcApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Pie()
        {
            var data = new PieChartModel()
            {
                Lables = new List<string>(){ "A", "B", "C" },
                Series = new List<int>(){10,40,50}
            };
            return View(data);
        }
        public IActionResult Radar()
        {
            RadarSeriseModel serie1 = new RadarSeriseModel()
            {
                name = "Player 1",
                data = new List<int> { 3,0,2,5,4}
            };

        var data = new RadarChartModel()
            {
                Series = new List<RadarSeriseModel>()
                {
                    serie1
                },
                Categories = new List<string>() { "Kill","Death","Assist","Survial","Heal"}
            };
            return View(data);
        }
    }
}
