using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Coding_Problems
{
    public class LoggerRateLimiter
    {
        static Dictionary<string, int> messageTimestamps = new Dictionary<string, int>();
        public static bool ShouldPrintMessage(int time, string message)
        {
            if (messageTimestamps.TryGetValue(message, out var lastTime))
            {
                if (time - lastTime < 10)
                    return false;
                else
                {
                    messageTimestamps[message] = time;
                    return true;
                }
            }
            else
            {
                messageTimestamps.Add(message, time);
                return true;
            }

        }
        public static void Run()
        {
            var isContinue = false;
            do
            {

                Console.WriteLine("Enter time and Message");
                Console.WriteLine("Enter time stamp:");
                var time = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Enter message:");
                var message = Console.ReadLine()!;
                var result = ShouldPrintMessage(time, message);
                if (result)
                {
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine("Message is not printed as it is rate limited.");
                }
                Console.WriteLine("Would you like to continue with logger or exit?\n1. Continue" +
                    "\t2. Anything else for exit");
                var input = int.Parse(Console.ReadLine()!);
                isContinue = input == 1;
            }
            while(isContinue);
        }
    }
}
