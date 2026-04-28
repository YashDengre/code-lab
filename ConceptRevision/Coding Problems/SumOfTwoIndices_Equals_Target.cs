using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Coding_Problems
{

    public class SumOfTwoIndices_Equals_Target
    {
        // Given an array of integers, return indices of the two numbers whose sum equals target
        // Exmaple: nums = [2, 7, 11, 15], target = 9
        // Output: [0, 1] because nums[0] + nums[1] = 2 + 7 = 9

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
        public static int[] Get_Indices_For_Equals_Target(List<int> arr, int target=0)
        {
            var indexMap = new Dictionary<int, int>();
            var indices = (0, 0);
            for (int i = 0; i < arr.Count; i++)
            {
                var complement = target - arr[i];
                if (indexMap.ContainsKey(complement))
                {
                    indices.Item1 = indexMap[complement];
                    indices.Item2 = i;
                    break;
                }
                else
                {
                    indexMap.Add(arr[i], i);
                }
            }

            return [indices.Item1, indices.Item2];
        }

        public static void Run(List<int>? nums = null, int target = 0)
        {

            if (nums == null || target == 0)
            {
                Console.WriteLine("Enter size of the array:");
                var size= int.Parse(Console.ReadLine()!);
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
            var result = SumOfTwoIndices_Equals_Target.Get_Indices_For_Equals_Target(nums!, target);
            Console.WriteLine($"Indices: [{result[0]}, {result[1]}]");
        }
    }
}
