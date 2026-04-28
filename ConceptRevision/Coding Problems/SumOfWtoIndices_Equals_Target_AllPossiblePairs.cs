using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Coding_Problems
{
    public class SumOfWtoIndices_Equals_Target_AllPossiblePairs
    {
        // Given an array of integers, return all possible pair of indices of the two numbers whose sum equals target
        // Exmaple: nums = [1,2,3,4,5,6], target = 7
        // Output: [0, 5] [1,4] [2,3] because nums[0] + nums[5] = 1 + 6 = 7 and so one.

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
        public static List<List<int>> Get_Indices_For_Equals_Target_AllPossiblePairs(List<int> arr, int target = 0)
        {
            var indexMap = new Dictionary<int, List<int>>();
            var pairs = new List<List<int>>();
            for (int i = 0; i < arr.Count; i++)
            {
                var complement = target - arr[i];
                if (indexMap.ContainsKey(complement))
                {
                    var value = indexMap[complement];
                    value.ForEach(index => pairs.Add(new List<int> { index, i }));
                }

                if (!indexMap.ContainsKey(arr[i]))
                {
                    indexMap[arr[i]] = new List<int>();
                }

                indexMap[arr[i]].Add(i);

                //if (indexMap.ContainsKey(arr[i]))
                //    indexMap[arr[i]].Add(i);
                //    //indexMap[arr[i]]= indexMap[arr[i]].Append(i).ToList();

                //else
                //    indexMap.Add(arr[i], new List<int>() { i });

            }

            return pairs;
        }

        public static List<List<int>> GetPairsByTwoPointers(List<int> nums, int target = 0)
        {
            var result = new List<List<int>>();

            nums.Sort();

            int left = 0;
            int right = nums.Count - 1;

            while (left < right)
            {
                int sum = nums[left] + nums[right];
                if (sum == target)
                {
                    result.Add(new List<int>() { nums[left], nums[right] });
                    int leftValue = nums[left];
                    int rightValue = nums[right];
                    while (left < right && nums[left] == leftValue)
                        left++;
                    while (left < right && nums[right] == rightValue)
                        right--;
                }
                else if (sum < target)
                {
                    left++;
                }
                else { right--; }
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
            Console.WriteLine("Choose an approach:");
            Console.WriteLine("1. HashMap/Dictionary\t2. Two Pointers");
            var options = int.Parse(Console.ReadLine()!);
            var result =  new List<List<int>>();
            switch (options)
            {
                case 1:
                    result = Get_Indices_For_Equals_Target_AllPossiblePairs(nums!, target);
                    break;
                case 2:
                    result = GetPairsByTwoPointers(nums!, target);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }

            Console.WriteLine("Pairs of indices whose sum equals target:");
            foreach (var pair in result)
            {
                Console.WriteLine($"Indices: [{pair[0]}, {pair[1]}]");
            }
        }
    }
}
