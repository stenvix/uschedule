using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Quartz;
using USchedule.Parser.Base;

namespace USchedule.Parser.Executor
{
    public class ParseJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var jobData = context.JobDetail.JobDataMap;
            var task = jobData.Get(ConstKeys.JobTask) as ParseTask;
            var scheduler = jobData.Get(ConstKeys.JobScheduler) as IScheduler;
            var logger = jobData.Get(ConstKeys.JobLogger) as ILogger<ParseJob>;
            var baseUrl = jobData.GetString(ConstKeys.JobBaseUrl);
            
            if (task == null || scheduler == null || logger == null || string.IsNullOrEmpty(baseUrl))
            {
                return;
            }

            var nextTasks = task.Action.Invoke(await GetDocument(baseUrl, task.Url), task.Args);
            
            foreach (var nextTask in nextTasks)
            {
                var nextJob = BuildJob(baseUrl, nextTask, scheduler, logger);
                await scheduler.ScheduleJob(nextJob, BuildTrigger(nextJob.Key));
            }
        }

        public static IJobDetail BuildJob(string baseUrl, ParseTask task, IScheduler scheduler, ILogger<ParseJob> logger)
        {
            var newJobData = new JobDataMap
            {
                {ConstKeys.JobBaseUrl, baseUrl},
                {ConstKeys.JobTask, task},
                {ConstKeys.JobScheduler, scheduler},
                {ConstKeys.JobLogger, logger}
            };
            var nextTaskId = Guid.NewGuid();
            
            return JobBuilder.Create<ParseJob>().WithIdentity(nextTaskId.ToString(), "ParseJob")
                .UsingJobData(newJobData).StoreDurably(true).Build();
        }

        public static ITrigger BuildTrigger(JobKey jobKey)
        {
            return TriggerBuilder.Create().StartNow().ForJob(jobKey).Build();
        }

        public static async Task<HtmlDocument> GetDocument(string baseUrl, string url)
        {
            var httpClient = HttpClientFactory.Create();
            httpClient.BaseAddress = new Uri(baseUrl);
            var response = await httpClient.GetAsync(url);
            var stream = await response.Content.ReadAsStreamAsync();

            HtmlDocument document = new HtmlDocument();
            document.Load(stream);
            return document;
        }
    }
}