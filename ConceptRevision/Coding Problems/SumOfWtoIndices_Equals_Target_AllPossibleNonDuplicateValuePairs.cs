using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Coding_Problems
{
    public class SumOfWtoIndices_Equals_Target_AllPossibleNonDuplicateValuePairs
    {
        // Given an array of integers, return all possible pair of indices of the two numbers whose sum equals target
        // Exmaple: nums = [3,3,3,3], target = 6
        // Output: [3,3] because 3 + 3 = 7

        // Clarification:
        // 1. Can we assume that there is exactly one solution?
        // Yes, we can assume that there is exactly one solution.

        // 2. Is the array sorted?
        // No, assume the array is not sorted.

        // 3. Can the array contains duplicates?
        // Yes, duplicates are allowed.


        // Define your approach:
        // Think about the Time complexity.
        // Think about the Space complexity.
        // Explain about the appraoch you are thinking about and then implement the code.
        // Approach:    I will use a dictionary to store numbers we have already seen.
        //              The key will be the number and the value will be its index.
        // Algorithm:   1. Iterate through the array.
        //              2. For each number, compute the complement (complement = target - current number).
        //              3. Check if the complement already exists in the dictionary.
        //              4. If it exists, we found the pair.
        //              5. Otherwise store the current number in the dictionary.
        public static List<List<int>> Get_Indices_For_Equals_Target_AllPossibleUniqueValuePairs(List<int> arr, int target = 0)
        {
            var seenValues = new HashSet<int>();
            var uniquePairs = new HashSet<string>();
            var indexMap = new Dictionary<int, List<int>>();
            var result = new List<List<int>>();
            for (int i = 0; i < arr.Count; i++)
            {
                var complement = target - arr[i];
                if (seenValues.Contains(complement))
                {
                    int first = Math.Min(arr[i], complement);
                    int second = Math.Max(arr[i], complement);
                    string pairKey = $"{first},{second}";
                    if(!uniquePairs.Contains(pairKey))
                    {
                        result.Add(new List<int> { first, second });
                        uniquePairs.Add(pairKey);
                    }
                }

               seenValues.Add(arr[i]);

            }

            return result;
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
            var result = SumOfWtoIndices_Equals_Target_AllPossibleNonDuplicateValuePairs.Get_Indices_For_Equals_Target_AllPossibleUniqueValuePairs(nums!, target);

            Console.WriteLine("Pairs of values whose sum equals target:");
            foreach (var pair in result)
            {
                Console.WriteLine($"Values: [{pair[0]}, {pair[1]}]");
            }
        }
    }
}
