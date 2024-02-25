namespace KKDotNetCore.WeatherForecastRepoPattern.Services.WeatherForecastService
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get();
    }
}
