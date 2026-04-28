namespace ConceptRevision.Coding_Problems
{
    public class Node
    {
        public int Key { get; set; }
        public int Value { get; set; }
        public Node? Previous { get; set; }
        public Node? Next { get; set; }
        public Node(int key, int value)
        {
            Key = key;
            Value = value;
            Previous = null;
            Next = null;
        }
    }
    public class LeastRecentlyUsed
    {
        #region Problem
        /*
        Problem: LRU Cache
        LRU means:
        Least Recently Used
        You need to design a cache with two operations:

        Operations
        Get(key)
        Put(key, value)
        
        Example
        Capacity = 2

        put(1,1)
        put(2,2)
        get(1) → returns 1
        put(3,3)
        get(2) → returns -1

        Why?
        Because 2 was the least recently used and got removed.

        Visual Flow
        capacity = 2

        Step 1
        put(1,1)
        Cache:
        1

        Step 2
        put(2,2)
        Cache:
        2 → 1

        Step 3
        get(1)
        Cache becomes:
        1 → 2
        Because 1 is now most recently used.

        Step 4
        put(3,3)
        Cache is full.
        So we remove:
        Least Recently Used → 2

        Final cache
        3 → 1
         */
        #endregion  
        public static Dictionary<int, Node> Cache = null!;
        public static Node? Head;
        public static Node? Tail;

        public static int CacheSize = 0;

        public static int Get(int key)
        {
            if (Cache.TryGetValue(key, out var node))
            {
                var value = node.Value;
                DetechNode(node);
                AddToHead(node);
                return value;
            }
            else { return -1; }
        }
        public static void Put(int key, int value)
        {
            if (Cache.TryGetValue(key, out var existingNode))
            {
                UpdateNode(existingNode, value);
            }
            else
            {
                if (CacheSize <= 0)
                {
                    RemoveTailNode();
                }
                var node = new Node(key, value);
                AddToHead(node);
                Cache.Add(key, node);
                CacheSize--;
            }
        }

        public static void Setup(int capacity)
        {
            Cache = new Dictionary<int, Node>();
            Head = new Node(int.MinValue, -1);
            Tail = new Node(int.MaxValue, -1);
            Head.Next = Tail;
            Tail.Previous = Head;
            CacheSize = capacity;
        }
        public static void Reset(int newCapacity = 0)
        {
            if(newCapacity != 0)
                CacheSize = newCapacity;
            Setup(CacheSize);
        }

        private static void RemoveTailNode()
        {
            var tailNode = Tail.Previous;
            DetechNode(tailNode);
            Cache.Remove(tailNode.Key);
            tailNode = null;
            CacheSize++;
        }
        private static void UpdateNode(Node node, int value)
        {
            node.Value = value;
            DetechNode(node);
            AddToHead(node);
        }

        private static void DetechNode(Node node)
        {
            var prev = node.Previous;
            var next = node.Next;
            if (prev != null)
                prev.Next = next;
            if (next != null)
                next.Previous = prev;
        }
        private static void AddToHead(Node node)
        {
            var lastRecentNode = Head.Next;
            node.Next = lastRecentNode;
            node.Previous = lastRecentNode.Previous;
            lastRecentNode.Previous = node;
            Head.Next = node;
        }

        public static void Run()
        {
            Console.WriteLine("Enter capacity of the cache:");
            var capacity =  int.Parse(Console.ReadLine()!);
            CacheSize = capacity;
            var continueOperation = true;
            Setup(capacity);
            while (continueOperation)
            {
                Console.WriteLine("Enter operation: 1. get\t2. put\t3. Reset with new capacity"+
                    "\t4. Enter 4 or above to end the operation.");
                var operation = int.Parse(Console.ReadLine()!);
                if (operation == 1)
                {
                    RetrieveData();
                }
                else if (operation == 2)
                {
                    InsertData();
                }
                else if (operation == 3)
                {
                    Reset(int.Parse(Console.ReadLine()!));
                }
                else
                {
                    Console.WriteLine("Exiting the operation.");
                    continueOperation = false;
                }
            }
        }
        private static void RetrieveData()
        {
            Console.WriteLine("Enter the key to retrieve value:");
            var key = int.Parse(Console.ReadLine()!);
            Console.WriteLine($"Retrieving data for key: {key}");
            Console.WriteLine($"value: {Get(key)}");
        }
        private static void InsertData()
        {
            Console.WriteLine("Enter the key and value:");
            Console.WriteLine("Key: ");
            var key = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Value: ");
            var value = int.Parse(Console.ReadLine()!);
            Put(key, value);
        }
    }
}
