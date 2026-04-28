namespace ConceptRevision.Coding_Problems
{
    public class LongestSubStringsWithAtMostKDistinctCharacters
    {
        //  string: abcbcdefg
        //  K: 3
        //  Goal: Find longest substring with at most K distinct characters.
        //  Output: abcbc
        public static string LongestSubstring(string inputString, int k = 0)
        {
            // We will use sliding window technique

            int start = 0;
            int end = 0;
            int maxLength = 0;
            string longestSubString = "";
            int strLen = inputString.Length;
            int maxStart = 0;
            var uniqueAtMostK = new Dictionary<char, int>();

            for (end = 0; end < strLen; end++)
            {
                // Expand the window by adding nums[end]
                char c = inputString[end];

                if (!uniqueAtMostK.ContainsKey(c))
                    uniqueAtMostK.Add(c, 0);
                uniqueAtMostK[c]++;

                while (uniqueAtMostK.Count > k)
                {
                    char startChar = inputString[start];
                    uniqueAtMostK[startChar]--;

                    if (uniqueAtMostK[startChar] == 0)
                        uniqueAtMostK.Remove(startChar);

                    start++;
                }

                if (end - start + 1 > maxLength)
                {
                    maxLength = end - start + 1;
                    maxStart = start;
                }
            }

            //Console.WriteLine($"Length: {len}");
            return inputString.Substring(maxStart, maxLength);
        }

        public static void Run(string input = "", int k = 0)
        {

            if (string.IsNullOrWhiteSpace(input) || k == 0)
            {
                Console.WriteLine("Enter string:");
                input = Console.ReadLine()!;

                Console.WriteLine("Enter K: At most unique:");
                k = int.Parse(Console.ReadLine()!);
            }

            var result = LongestSubstring(input, k);


            Console.WriteLine($"Longest SubString with at most K distinct elements:{result}");

        }
    }
}
