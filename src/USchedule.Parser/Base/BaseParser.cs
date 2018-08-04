using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using USchedule.Parser.Executor;

namespace USchedule.Parser.Base
{
    public abstract class BaseParser
    {
        private readonly string _baseUrl;

        protected readonly ILogger<BaseParser> Logger;
        private readonly ILogger<ParseJob> _parseLogger;

        protected BaseParser(string baseUrl, ILogger<BaseParser> logger, ILogger<ParseJob> parseLogger)
        {
            _baseUrl = baseUrl;
            Logger = logger;
            _parseLogger = parseLogger;
        }


        protected abstract IEnumerable<ParseTask> InitialTask(HtmlDocument document);

        public async Task RunAsync()
        {
            var factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();
            var tasks = InitialTask(await ParseJob.GetDocument(_baseUrl, ""));

            foreach (var task in tasks)
            {
                var job = ParseJob.BuildJob(_baseUrl, task, scheduler, _parseLogger);
                await scheduler.ScheduleJob(job, ParseJob.BuildTrigger(job.Key));
            }

            await scheduler.Start();
        }
    }
}