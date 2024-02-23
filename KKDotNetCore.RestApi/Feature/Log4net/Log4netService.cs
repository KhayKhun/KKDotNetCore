using log4net;
using log4net.Config;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace KKDotNetCore.RestApi.Feature.Log4net
{
    public static class Log4netExtensions
    {
        public static void AddLog4net(this IServiceCollection services)
        {
            // Resolve the absolute path to log4net.config
            string log4netConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), "log4net.config");

            // Configure log4net using the absolute path
            XmlConfigurator.Configure(new FileInfo(log4netConfigFilePath));

            // Add the logger as a singleton service
            services.AddSingleton(LogManager.GetLogger(typeof(Program)));
        }
    }
}
