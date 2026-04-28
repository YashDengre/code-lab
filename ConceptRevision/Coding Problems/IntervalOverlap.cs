namespace ConceptRevision.Coding_Problems
{
    public class IntervalOverlap
    {
        //  Next Greater Element (Stack Pattern)
        //Input:  [2, 1, 2, 4, 3]
        //Output: [4, 2, 4, -1, -1]
        /*
         2 → next greater = 4
         1 → next greater = 2
         2 → next greater = 4
         4 → none → -1
         3 → none → -1
         */
        public static List<List<int>> FindOverlap(List<List<int>> intervals)
        {
            var result = new List<List<int>>();

            if (intervals.Count == 0)
                return result;

            // Array.Sort(intervals.ToArray(), (a, b) => a[0].CompareTo(b[0]));
            intervals.Sort((a, b) => a[0].CompareTo(b[0]));

            List<int> current = intervals[0];

            foreach(var interval in intervals)
            {
                if (interval[0] <= current[1])
                {
                    current[1] = Math.Max(interval[1], current[1]);
                }
                else
                {
                    result.Add(current);
                    current = interval;
                }
            }
            result.Add(current);
            
            return result;
        }

        public static void Run(List<List<int>>? intervals = null)
        {
            if (intervals == null)
            {
                Console.WriteLine("Enter size of the interval array:");
                var size = int.Parse(Console.ReadLine()!);
                intervals = new List<List<int>>();
                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine($"Enter start time for {i}:");
                    var start = int.Parse(Console.ReadLine()!);
                    Console.WriteLine($"Enter end time for {i}:");
                    var end = int.Parse(Console.ReadLine()!);
                    var interval = new List<int>() { start, end};
                    intervals.Add(interval);
                }
            }

            var result = FindOverlap(intervals!);

            foreach (var interval in result)
            {
                Console.Write($"[{interval[0]},{interval[1]}] ");
            }
        }
    }
}
