namespace ConceptRevision.Coding_Problems
{
    public class NextGreaterElement
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
        public static int[] NextGreater(List<int> nums)
        {
            var result = new int[nums.Count];
            for (int i = 0; i < nums.Count; i++)
            {
                result[i] = -1;
            }
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < nums.Count; i++) //i=0 1 2 3 4
            {
                while (stack.Count > 0 && nums[i] > nums[stack.Peek()]) 
                {
                    var index = stack.Pop(); // 2 0
                    result[index] = nums[i]; // [4,2,4,-1,-1]
                }
                stack.Push(i); // stack: 3
            }
            return result;
        }

        public static void Run(List<int>? nums = null)
        {
            if (nums == null)
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
            }

            var result = NextGreater(nums!);

            foreach (var element in result)
            {
                Console.Write($"{element} ");
            }

        }
    }
}
