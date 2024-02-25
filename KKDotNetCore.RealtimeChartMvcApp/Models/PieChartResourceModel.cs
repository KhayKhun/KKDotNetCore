namespace KKDotNetCore.RealtimeChartMvcApp.Models
{
    public class PieChartResourceModel
    {
        public PieChartModel Data {  get; set; }
    }
    public class PieChartModel
    {
        public List<int> Series { get; set; }
        public List<string> Labels { get; set; }
    }
}
