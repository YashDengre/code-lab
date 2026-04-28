
namespace ConceptRevision.Coding_Problems
{
    public class SlidingWindowMaximum
    {
        public static List<int> MaximumOfWindowBruteForce(List<int> nums, int k)
        {
            List<int> result = new List<int>();

            for (int i = 0; i <= nums.Count - k; i++)
            {
                int max = int.MinValue;

                for (int j = i; j < i + k; j++)
                {
                    if (nums[j] > max)
                        max = nums[j];
                }
                result.Add(max);
            }

            return result;
        }

        public static List<int> MaximumOfWindowDeque(List<int> nums, int k)
        {
            var result= new List<int>();
            LinkedList<int> deque = new LinkedList<int>();

            for (int i = 0; i < nums.Count; i++) { 

                // Remove elements out of window
                if(deque.Count > 0 && deque.First.Value <= i - k)
                {
                    deque.RemoveFirst();
                }
                // Maintain Decreasing order in the deque
                while(deque.Count>0 && nums[deque.Last.Value] < nums[i])
                {
                    deque.RemoveLast();
                }

                // Add current element index to the deque
                deque.AddLast(i);
                if(i>= k - 1)
                {
                    result.Add(nums[deque.First.Value]);
                }
            }

            return result;
        }
        public static void Run()
        {
            Console.WriteLine("Enter the size of array");
            int size = int.Parse(Console.ReadLine()!);
            var nums = new List<int>();
            for (int i = 1; i <= size; i++)
            {
                Console.WriteLine($"Enter element for Key{i}");
                int element = int.Parse(Console.ReadLine()!);
                nums.Add(element);
            }
            Console.WriteLine("Enter the size of window");
            int k = int.Parse(Console.ReadLine()!);
            
            //var result = MaximumOfWindowBruteForce(nums, k);
            var result = MaximumOfWindowDeque(nums, k);

            Console.Write("[ ");
            foreach(var max in result)
            {
                Console.Write($"{max} ");
            }
            Console.WriteLine("]");
        }
    }
}
