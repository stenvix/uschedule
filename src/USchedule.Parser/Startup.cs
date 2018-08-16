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
            var teachersLogger = LoggerFactory.CreateLogger<NulpStudentsParser>();
            var teacherParseLogger = LoggerFactory.CreateLogger<ParseJob>();
            var teachersUrl = Config.GetValue<string>("Nulp:TeachersUrl");
            var teachersApiUrl = Config.GetValue<string>("Nulp:TeachersApiUrl");

            var teacherParser = new NulpTeachersParser(teachersUrl, teachersApiUrl, teachersLogger, teacherParseLogger);
            teacherParser.RunAsync().GetAwaiter().GetResult();

//            var logger = LoggerFactory.CreateLogger<NulpStudentsParser>();
//            var parseLogger = LoggerFactory.CreateLogger<ParseJob>();
//            var studentsUrl = Config.GetValue<string>("Nulp:StudentsUrl");
//            var studentsApiUrl = Config.GetValue<string>("Nulp:StudentsApiUrl");
//            var parser = new NulpStudentsParser(studentsUrl, studentsApiUrl, logger, parseLogger);
//            parser.RunAsync().GetAwaiter().GetResult();
        }
    }
}