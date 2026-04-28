namespace ConceptRevision.Coding_Problems
{
    public class TopKFrequentElements
    {
        /*
        Let's move to another extremely common interview problem:
        Top K Frequent Elements
        
        Example:
        nums = [1,1,1,2,2,3]
        k = 2

        Output:
        [1,2]
        
        Because:
        1 appears 3 times
        2 appears 2 times
         */
        public static List<int> TopKFrequent(List<int> nums, int k)
        {
            var result = new List<int>();

            var elementFrequency = new Dictionary<int, int>();

            foreach (var num in nums)
            {
                if (elementFrequency.ContainsKey(num))
                {
                    elementFrequency[num]++;
                }
                else
                {
                    elementFrequency.Add(num, 1);
                }
            }
            
            elementFrequency.OrderByDescending(kv => kv.Value)
                .Take(k)
                .ToList()
                .ForEach(kv => result.Add(kv.Key));
            return result.ToList();
        }
        public static List<int> TopKFrequentWithBucketSort(List<int> nums, int k)
        {
            var result = new List<int>();

            var elementFrequency = new Dictionary<int, int>();

            foreach (var num in nums)
            {
                if (elementFrequency.ContainsKey(num))
                {
                    elementFrequency[num]++;
                }
                else
                {
                    elementFrequency.Add(num, 1);
                }
            }

            // Bucket sort:
            List<int>[] buckets = new List<int>[nums.Count+1];
            foreach(var pair in elementFrequency)
            {
                int freq = pair.Value;
                if (buckets[freq] == null)
                {
                    buckets[freq] = new List<int>();
                }
                buckets[freq].Add(pair.Key);
            }

            // Collect top k frequent elements from the buckets:

            for(int i=buckets.Length-1; i>=0 && result.Count < k; i--)
            {
                if (buckets[i] != null)
                {
                    result.AddRange(buckets[i]);
                }
            }

            return result.Take(k).ToList();
        }

        public static void Run(List<int>? nums = null, int k=0)
        {
            if (nums == null)
            {
                Console.WriteLine("Enter size of the array:");
                var size = int.Parse(Console.ReadLine()!);
                nums = new List<int>();
                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine($"Enter element for {i}:");
                    var value = int.Parse(Console.ReadLine()!);
                    nums.Add(value);
                }
                Console.WriteLine("Enter k:");
                k = int.Parse(Console.ReadLine()!);
            }

            var result = new List<int>();

            Console.WriteLine("Version: 1. Default Sort MS\t2. Bucket Sort Manually");
            var option = int.Parse(Console.ReadLine()!);
            if (option == 1)
            {
                result = TopKFrequent(nums!, k);
            }
            else if (option == 2)
            {
                result = TopKFrequentWithBucketSort(nums, k);
            }
            else
            {
                Console.WriteLine("Invalid option. Please select either 1 or 2.");
            }

            Console.Write("[");
            foreach (var interval in result)
            {
                Console.Write($"{interval} ");
            }
            Console.Write("]");
        }
    }
}
