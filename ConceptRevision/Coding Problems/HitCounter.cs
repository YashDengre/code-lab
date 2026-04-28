using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Coding_Problems
{
    public class HitCounter
    {
        static Queue<int> Hits = new Queue<int>();
        static int timePeriod = 300; // 5 minuts

        public static void Hit(int timeStamp)
        {
            Hits.Enqueue(timeStamp);
        }
        public static int GetHits(int timeStamp)
        {
            while (Hits.Count > 0 && Hits.Peek() <= timeStamp - timePeriod)
            {
                Hits.Dequeue();
            }

            return Hits.Count;

        }
        public static void Run()
        {
            var isContinue = false;
            do
            {
                Console.WriteLine("1. Hit\t2. GetHits");
                var option = int.Parse(Console.ReadLine()!);
                if (option == 1)
                {
                    Console.WriteLine("Enter Hit-Timestamp");
                    var time = int.Parse(Console.ReadLine()!);
                    Hit(time);
                }
                if (option == 2)
                {
                    Console.WriteLine("Enter timestamp to get hits");
                    var time = int.Parse(Console.ReadLine()!);
                    Console.WriteLine($"Hits: {GetHits(time)}");
                }

                Console.WriteLine("\n\nWould you like to continue Hit Counter\n1. Continue" +
                    "\t2. Anything else for exit");
                var input = int.Parse(Console.ReadLine()!);
                isContinue = input == 1;
            }
            while (isContinue);
        }
    }
}
