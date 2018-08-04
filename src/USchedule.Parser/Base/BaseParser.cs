using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace USchedule.Parser.Base
{
    public abstract class BaseParser
    {
        protected readonly ILogger<BaseParser> Logger;
        protected HttpClient HttpClient;

        protected BaseParser(string baseUrl, ILogger<BaseParser> logger)
        {
            Logger = logger;
            HttpClient = new HttpClient{BaseAddress = new Uri(baseUrl)};
        }

        protected abstract IEnumerable<ParseTask> InitialTask(HtmlDocument document);

        public async Task RunAsync()
        {
            var startResponse = await HttpClient.GetAsync("");
            var startStream = await startResponse.Content.ReadAsStreamAsync();

            HtmlDocument startDoc = new HtmlDocument();
            startDoc.Load(startStream);
                        
            var tasks = InitialTask(startDoc);
            await ProcessQueue(tasks);
        }

        private async Task ProcessQueue(IEnumerable<ParseTask> queue)
        {
            foreach (var task in queue)
            {
                try
                {
                    var response = await HttpClient.GetAsync(task.Url);
                    var stream = await response.Content.ReadAsStreamAsync();

                    HtmlDocument doc = new HtmlDocument();
                    doc.Load(stream);
                    await ProcessQueue(task.Action.Invoke(doc, task.Args));
                }
                catch (Exception e)
                {
                    Logger.LogError(e, e.Message);
                    throw;
                }
            }
        }
    }
}