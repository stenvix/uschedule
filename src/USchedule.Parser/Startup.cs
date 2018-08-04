using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using USchedule.Parser.Executor;

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
            LoggerFactory.AddNLog();
        }

        public void Run()
        {
            var logger = LoggerFactory.CreateLogger<NulpParser>();
            var parseLogger = LoggerFactory.CreateLogger<ParseJob>();
            var parser = new NulpParser(logger, parseLogger);
            parser.RunAsync().GetAwaiter().GetResult();
        }
    }
}