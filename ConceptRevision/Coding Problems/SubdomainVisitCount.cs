namespace ConceptRevision.Coding_Problems
{
    public class SubdomainVisitCount
    {
        public static List<string> SubDomainVisit(List<string> cpdomains)
        {
            var visit = new Dictionary<string, int>();

            foreach (var cpdomain in cpdomains)
            {
                var splits = cpdomain.Split(' ');
                int count = int.Parse(splits[0]);
                string domain = splits[1];
                var subDomains = domain.Split(".");

                for (int i = 0; i < subDomains.Length; i++)
                {
                    string subDomain = string.Join(".", subDomains.Skip(i));

                    if (visit.ContainsKey(subDomain))
                    {
                        visit[subDomain] += count;
                    }
                    else
                    {
                        visit.Add(subDomain, count);
                    }
                }
            }
            List<string> result = new List<string>();
            foreach (var pair in visit)
            {
                result.Add($"{pair.Value} {pair.Key}");
            }
            return result;
        }
        public static void Run()
        {
            Console.WriteLine("Enter the size of the domain's array");
            var size = int.Parse(Console.ReadLine()!);
            var domains = new List<string>();
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"Enter visit and domain for index {i} and separate by space");
                var domain = Console.ReadLine();
                domains.Add(domain!);
            }
            var result = SubDomainVisit(domains);

            foreach (var domain in result)
            {
                Console.WriteLine($"{domain}");
            }
        }
    }
}
