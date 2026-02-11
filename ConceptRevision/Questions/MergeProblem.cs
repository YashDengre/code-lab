namespace ConceptRevision.Questions
{
    public class MergeProblem
    {
        public static void Process()
        {
            var nums1 = new int[] { 2, 0 };
            var nums2 = new int[] { 1 };
            Solution.Merge(nums1, 1, nums2, 1);
            Console.WriteLine();

            //var nums1 = new int[] { 1, 2, 3, 0, 0, 0 };
            //var nums2 = new int[] { 2, 5, 6 };
            //Solution.Merge(nums1, 3, nums2, 3);
            Console.WriteLine();
        }

    }

    public static class Solution
    {
        public static void Merge(int[] nums1, int m, int[] nums2, int n)
        {

            var len = m + n;
            var lenA = nums1.Length;
            var lenB = nums2.Length;
            var resultedArray = new int[len];
            Array.Copy(nums1, resultedArray, len);
            var lenR = 0;

            if (lenA == 0)
            {
                nums1 = nums2;
            }
            else if (lenB == 0)
            {
                nums2 = nums1;
            }
            else
            {
                for (int i = 0, j = 0; i < lenA && j < lenB && lenR < len;)
                {
                    if (resultedArray[i] < nums2[j] && resultedArray[i] != 0)
                    {
                        nums1[lenR] = resultedArray[i];
                        i = i == lenA - 1 ? i : i + 1;
                    }
                    else
                    {
                        nums1[lenR] = nums2[j];
                        j = j == lenB - 1 ? j : j + 1;
                    }
                    lenR = lenR + 1;
                }

            }

        }
    }
}
