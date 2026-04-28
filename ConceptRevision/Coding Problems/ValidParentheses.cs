namespace ConceptRevision.Coding_Problems
{
    public class ValidParentheses
    {
        //  string: ()[]{}
        //  Goal: if all the brackets are openinig and closing .
        //  Output: True
        public static bool Validate(string inputString)
        {
            //isValid = string.IsNullOrEmpty(inputString) ? true : false;

            var dict = new Dictionary<char, char>
            {
                { ')', '(' },
                { '}', '{' },
                { ']', '[' }
            };

            if (inputString.Length % 2 != 0)
            {
                return false;
            }
            Stack<char> stack = new Stack<char>();
            foreach (char c in inputString)
            {
                if (dict.ContainsKey(c))
                {
                    if (stack.Count > 0 && stack.Pop() != dict[c])
                        return false;
                }
                else
                {
                    stack.Push(c);
                }
            }
            return stack.Count == 0;
        }

        public static void Run(string input = "")
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Enter string:");
                input = Console.ReadLine()!;
            }

            var result = Validate(input);

            Console.WriteLine($"Is Valid: {result}");

        }
    }
}
