using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace USchedule.Parser.Base
{
    public class ParseTask
    {
        public readonly Func<HtmlDocument, Dictionary<string, string>, IEnumerable<ParseTask>> Action;
        public readonly string Url;
        public readonly Dictionary<string, string> Args;

        public ParseTask(Func<HtmlDocument, Dictionary<string, string>, IEnumerable<ParseTask>> action, string url, Dictionary<string, string> args)
        {
            Action = action;
            Url = url;
            Args = args;
        }
    }
}