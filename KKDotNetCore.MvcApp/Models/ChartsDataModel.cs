using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace KKDotNetCore.MvcApp
{
    public class PieChartModel
    {
        public List<int> Series { get; set; }
        public List<string> Lables { get; set; }
    }

    public class RadarSeriseModel
    {
        public string name { get; set; }
        public List<int> data { get; set; }

    }
    public class RadarChartModel
    {
        public List<RadarSeriseModel> Series { get; set; }
        public List<string> Categories { get; set; }
    }

    public class ScatterDotPosition
    {
        public double x { get; set; }
        public double y { get; set; }
    }

    public class ScatterChartModel
    {
        public List<ScatterDotPosition> data1 { get; set; }
        public List<ScatterDotPosition> data2 { get; set; }
    }
    public class HoriBarDataSetModel
    {
        public string label { get; set; }
        public List<int> data { get; set; }
        public string borderColor { get; set; }
        public string backgroundColor { get; set; }

    }
    public class HoriBarModel
    {
        public List<string> labels { get; set; }
        public List<HoriBarDataSetModel> datasets { get; set; }
    }

    public class SemiCircleModel
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public List<List<object>> Data { get; set; }

    }

    public class DataPointsModel
    {
		public string label { get; set; }
		public double y { get; set; }
	}

    public class PyramidModel
    {
        public string Title { get; set; }
        public List<DataPointsModel> DataPoints { get; set; }
    }

    public class RangeAreaDataPoint
    {
        public List<int> y { get; set; }
        public DateTime x { get; set; }
    }

    public class RangeAreaModel
    {
        public string Title { get; set;}
        public string TitleY { get; set;}
        public string SuffixY { get; set;}
        public string TitleX { get; set;}
        public List<RangeAreaDataPoint> DataPoints { get; set;}
    }

    public class TimeSeriesChartModel
    {
        public string Title { get; set; }
        public string TitleX { get; set; }
        public string TitleY { get; set; }
        public dynamic Data { get; set; }
        
    }
}
