using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace USchedule.Parser
{
    public class Startup
    {
        public IConfigurationRoot Config { get; set; }
        public ILoggerFactory LoggerFactory { get; set; }

        public Startup()
        {
            
        }

        public void Configure()
        {
            SetConfiguration();
            SetLoggin();
        }

        private void SetConfiguration()
        {
            Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
        }

        private void SetLoggin()
        {

            LoggerFactory = new LoggerFactory();
            LoggerFactory.AddConsole(new ConfigurationConsoleLoggerSettings(Config.GetSection("Logging:Console")));
        }

        public void Run()
        {
            var logger = LoggerFactory.CreateLogger<NulpParser>();
            var parser = new NulpParser(logger);
            parser.RunAsync().GetAwaiter().GetResult();
        }
    }
}