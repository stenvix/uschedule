using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using USchedule.Parser.Executor;
using USchedule.Shared.Models;

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
            while (true)
            {
                Console.WriteLine("Choose what to parse:");
                Console.WriteLine("1: Teachers");
                Console.WriteLine("2: Students");
                Console.WriteLine("3: Teachers from file");
                Console.WriteLine("4: Students from file");
                var value = Console.ReadLine();
                switch (value)
                {
                    case "1":
                    {
                        var teacherParser = InitializeTeachersParser();
                        teacherParser.RunAsync().GetAwaiter().GetResult();
                        break;
                    }
                    case "2":
                    {
                        var parser = InitializeStudentsParser();
                        parser.RunAsync().GetAwaiter().GetResult();
                        break;
                    }
                    case "3":
                    {
                        
                        var path = Path.Join(Directory.GetCurrentDirectory(), "teachers.json");
                        if (File.Exists(path))
                        {
                            var teacherParser = InitializeTeachersParser();
                            var departmentsString = File.ReadAllText(path);
                            var departments = JsonConvert.DeserializeObject<IList<DepartmentSharedModel>>(departmentsString);
                            teacherParser.PostDataToServer(departments, false).GetAwaiter().GetResult();
                        }
                        else
                        {
                            Console.WriteLine("File does not exists, please run parser");
                        }
                        break;
                    }
                    case "4":
                    {
                        var path = Path.Join(Directory.GetCurrentDirectory(), "students.json");
                        if (File.Exists(path))
                        {
                            var teacherParser = InitializeStudentsParser();
                            var institutesString = File.ReadAllText(path);
                            var institutes = JsonConvert.DeserializeObject<IList<InstituteSharedModel>>(institutesString);
                            teacherParser.PostDataToServer(institutes, false).GetAwaiter().GetResult();
                        }
                        else
                        {
                            Console.WriteLine("File does not exists, please run parser");
                        }
                        break;
                    }
                    default:
                    {
                        return;
                    }
                }
            }
        }

        private NulpTeachersParser InitializeTeachersParser()
        {
            var teachersLogger = LoggerFactory.CreateLogger<NulpTeachersParser>();
            var teacherParseLogger = LoggerFactory.CreateLogger<ParseJob>();
            var teachersUrl = Config.GetValue<string>("Nulp:TeachersUrl");
            var teachersApiUrl = Config.GetValue<string>("Nulp:TeachersApiUrl");

            return new NulpTeachersParser(teachersUrl, teachersApiUrl, teachersLogger,
                teacherParseLogger);
        }

        private NulpStudentsParser InitializeStudentsParser()
        {
            var logger = LoggerFactory.CreateLogger<NulpStudentsParser>();
            var parseLogger = LoggerFactory.CreateLogger<ParseJob>();
            var studentsUrl = Config.GetValue<string>("Nulp:StudentsUrl");
            var studentsApiUrl = Config.GetValue<string>("Nulp:StudentsApiUrl");
            return new NulpStudentsParser(studentsUrl, studentsApiUrl, logger, parseLogger);
        }
    }
}