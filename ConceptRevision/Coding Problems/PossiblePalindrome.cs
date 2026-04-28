namespace ConceptRevision.Coding_Problems
{
    public class PossiblePalindrome
    {
        public static string CheckPalindrome(string inputStr)
        {
            int start = 0;
            int end = inputStr.Length - 1;
            var result = new char[inputStr.Length];

            for (; start <= end;)
            {
                if (inputStr[start] == inputStr[end])
                {
                    char replaceChar = inputStr[start];
                    if (replaceChar == '?')
                    {
                        replaceChar = 'a';
                    }
                    result[start] = replaceChar;
                    result[end] = replaceChar;
                }
                else if (inputStr[start] == '?' && inputStr[end] != '?')
                {
                    result[start] = inputStr[end];
                    result[end] = inputStr[end];
                }
                else if (inputStr[end] == '?' && inputStr[start] != '?')
                {
                    result[end] = inputStr[start];
                    result[start] = inputStr[start];
                }
                else
                {
                    return "-1";
                }
                start++;
                end--;
            }
            return new string(result);
        }
        public static void Run()
        {
            Console.WriteLine($"Enter the string for");
            var input = Console.ReadLine()!;
            var result = CheckPalindrome(input);

            Console.Write($"{input}: Palindrome is {result}");
        }
    }
}
