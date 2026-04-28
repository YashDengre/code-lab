namespace ConceptRevision.Coding_Problems
{
    public class MinimalSubArraySum_Equals_Target
    {
        //  Array: [2, 3, 1, 2, 4, 3]
        //  Target: 7
        //  Goal: Find minimal length subarray whose sum ≥ 7.
        public static int GetMinimalSubArrayLength(List<int> nums, int target = 0)
        {
            // We will use sliding window technique/Two points

            int start = 0;
            int end = 0;
            int minLen = int.MaxValue;
            int sum = 0;

            for (end = 0; end < nums.Count; end++)
            {
                // Expand the window by adding nums[end]
                sum = sum + nums[end];

                while(sum >= target)
                {
                    minLen = Math.Min(minLen,(end - start+1));
                    sum = sum - nums[start];
                    start++;
                }
            }
            return minLen == int.MaxValue ? 0 : minLen;
        }

        public static void Run(List<int>? nums = null, int target = 0)
        {

            if (nums == null || target == 0)
            {
                Console.WriteLine("Enter size of the array:");
                var size = int.Parse(Console.ReadLine()!);
                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine($"Enter element {i}:");
                    var element = int.Parse(Console.ReadLine()!);
                    nums ??= new List<int>();
                    nums.Add(element);
                }
                Console.WriteLine("Enter target:");
                target = int.Parse(Console.ReadLine()!);
            }

            var result = GetMinimalSubArrayLength(nums!, target);
           

            Console.WriteLine($"Minimal length of Sub Array Whose sum >= Target:{result}");
           
        }
    }
}
