using System;

namespace USchedule.Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var startup = new Startup();
            startup.Configure();
            startup.Run();
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}