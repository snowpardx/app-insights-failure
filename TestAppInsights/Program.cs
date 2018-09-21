using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.AspNetCore.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;

namespace TestAppInsights
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggerFactory = new LoggerFactory();

            var serviceCollection = new ServiceCollection()
                .AddSingleton<ILoggerFactory>(loggerFactory)
                .AddSingleton<IHostingEnvironment>(new HostingEnvironment() { ContentRootPath = Directory.GetCurrentDirectory() })
                .AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions
                {
                   DeveloperMode = true,
                   InstrumentationKey = "your instrumentation key",
                })
                .Configure<ApplicationInsightsLoggerOptions>(o => o.IncludeEventId = true);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            loggerFactory.AddApplicationInsights(serviceProvider, LogLevel.Trace);

            var logger = loggerFactory.CreateLogger("Sample logger");
            logger.LogWarning("Sample {data} logged", 12345);
        }
    }
}
