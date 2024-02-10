using Microsoft.AspNetCore.Mvc;

namespace KKDotNetCore.MvcApp.Controllers
{
    public class ChartJsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Scatter()
        {
            var data1 = new List<ScatterDotPosition>() {
                new ScatterDotPosition(){ x=-10,y=0},
                new ScatterDotPosition(){ x=0,y=10},
                new ScatterDotPosition(){ x=10,y=5},
                new ScatterDotPosition(){ x=0.5,y=5.5}
            };
            var data2 = new List<ScatterDotPosition>() {
                new ScatterDotPosition(){ x=-8,y=5},
                new ScatterDotPosition(){ x=3,y=3},
                new ScatterDotPosition(){ x=-6,y=2},
                new ScatterDotPosition(){ x=0.2,y=5}
            };

            ScatterChartModel Data = new ScatterChartModel()
            {
                data1 = data1,
                data2 = data2
            };

            return View(Data);
        }

        public IActionResult HoriBar()
        {
            var data = new HoriBarModel(){
                labels = new List<string>() { "Most loved languages"},
                datasets = new List<HoriBarDataSetModel>()
                {
                    new HoriBarDataSetModel()
                    {
                        label = "Rust",
                        data = new List<int>(){ 83 },
                        backgroundColor = "#8bca84",
                        borderColor = "#3b8132"
                    },
                    new HoriBarDataSetModel()
                    {
                        label = "Python",
                        data = new List<int>(){ 76 },
                        backgroundColor = "#8bca84",
                        borderColor = "#3b8132"
                    }
                    ,new HoriBarDataSetModel()
                    {
                        label = "Typescript",
                        data = new List<int>(){ 68 },
                        backgroundColor = "#8bca84",
                        borderColor = "#3b8132"
                    }
                    ,new HoriBarDataSetModel()
                    {
                        label = "Web Assembly",
                        data = new List<int>(){ 64 },
                        backgroundColor = "#8bca84",
                        borderColor = "#3b8132"
                    }
                    ,new HoriBarDataSetModel()
                    {
                        label = "Elixar",
                        data = new List<int>(){ 52 },
                        backgroundColor = "#8bca84",
                        borderColor = "#3b8132"
                    },
                }
            };
            return View(data);
        }
    }

}
