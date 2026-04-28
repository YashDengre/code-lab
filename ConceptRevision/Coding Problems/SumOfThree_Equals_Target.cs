namespace ConceptRevision.Coding_Problems
{
    public class SumOfThree_Equals_Target
    {
        public static List<List<int>> GetTripletsValuePair(List<int> nums, int target = 0)
        {
            var result = new List<List<int>>();
            var uniqueTriplets = new HashSet<string>();
            // outer loop to fix the first element of the triplet

            for (int i = 0; i < nums.Count - 2; i++)
            {
                int current = nums[i];
                var remainingTarget = target - current;

                var seenValues = new HashSet<int>();
                for (int j = i + 1; j < nums.Count; j++)
                {
                    int complement = remainingTarget - nums[j];
                    if (seenValues.Contains(complement))
                    {
                        // triplet found
                        var triplet = new List<int> { current, nums[j], complement };
                        triplet.Sort();
                        string key = $"{triplet[0]},{triplet[1]},{triplet[2]}";

                        if (!uniqueTriplets.Contains(key))
                        {
                            uniqueTriplets.Add(key);
                            result.Add(triplet);
                        }
                    }
                    seenValues.Add(nums[j]);
                }
            }
            return result;
        }

        public static List<List<int>> GetTripletsIndicesPair(List<int> nums, int target = 0)
        {
            var result = new List<List<int>>();
            var uniqueTriplets = new HashSet<string>();
            // outer loop to fix the first element of the triplet

            for (int i = 0; i < nums.Count - 2; i++)
            {
                int current = nums[i];
                var remainingTarget = target - current;

                // dictionary to store the indices of the numbers we have seen so far
                var indexMap = new Dictionary<int, List<int>>();
                for (int j = i + 1; j < nums.Count; j++)
                {
                    int complement = remainingTarget - nums[j];
                    if (indexMap.ContainsKey(complement))
                    {
                        var values = indexMap[complement];
                        values.ForEach(complementIndex =>
                        {
                            var tripletIndices = new List<int> { i, complementIndex, j };
                            tripletIndices.Sort();
                            string key = $"{tripletIndices[0]},{tripletIndices[1]},{tripletIndices[2]}";
                            if (!uniqueTriplets.Contains(key))
                            {
                                uniqueTriplets.Add(key);
                                result.Add(tripletIndices);
                            }
                        });

                    }
                    // Add current index to the dictionary
                    if (!indexMap.ContainsKey(nums[j]))
                        indexMap[nums[j]] = new List<int>();
                    indexMap[nums[j]].Add(j);
                }

            }

            return result;
        }

        public static List<List<int>> GetTripletsValuePairUsingTwoPointer(List<int> nums, int target = 0)
        {
            var triplets = new List<List<int>>();
            nums.Sort();
            for (int i = 0; i < nums.Count - 2; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1])
                    continue; // Skip duplicate elements
                int left = i + 1;
                int right = nums.Count - 1;

                while (left > right)
                { 
                    int sum = nums[i] + nums[left] + nums[right];
                    if (sum == target)
                    {
                        triplets.Add(new List<int> { nums[i], nums[left], nums[right] });

                        // skip duplicates for left and right

                        int leftValue = nums[left];
                        while(left < right && nums[left] == leftValue)
                            left++;
                        int rightValue = nums[right];
                        while(left < right && nums[right] == rightValue)
                            right--;
                    }
                    else if (sum < target)
                    {
                        left++;
                    }
                    else
                    {
                        right--;
                    }
                }
            }

            return triplets;

        }

        public static List<List<int>> GetTripletsBruteForce(List<int> arr, int target = 0)
        {
            var triplets = new List<List<int>>();
            for (int i = 0; i < arr.Count - 2; i++)
            {
                for (int j = i + 1; j < arr.Count - 1; j++)
                {
                    for (int k = j + 1; k < arr.Count; k++)
                    {
                        if (arr[i] + arr[j] + arr[k] == target)
                        {
                            triplets.Add(new List<int> { arr[i], arr[j], arr[k] });
                        }
                    }
                }
            }
            return triplets;
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

            Console.WriteLine("Version: 1. Indices\t2. Values\t3. Use Two Pointers");
            var result = new List<List<int>>();
            var options = int.Parse(Console.ReadLine()!);
            if (options == 1)
            {
                result = SumOfThree_Equals_Target.GetTripletsIndicesPair(nums!, target);
            }
            else if (options == 2)
            {
                result = SumOfThree_Equals_Target.GetTripletsValuePair(nums!, target);
            }
            else if (options == 3)
            {
            }
            else
            {
                Console.WriteLine("Invalid option. Please select either 1 or 2.");
            }
            //var result = SumOfThree_Equals_Target.GetTripletsBruteForce(nums!, target);

            Console.WriteLine("Pairs of values whose sum equals target:");
            foreach (var pair in result)
            {
                Console.WriteLine($"Values: [{pair[0]}, {pair[1]}, {pair[2]}]");
            }
        }
    }
}
