namespace ConceptRevision
{

    public class SingletonDesignPattern
    {
        public static void Process()
        {
            Consumer.SP.Process();
            Consumer.SPTS.Process();
            Consumer.SPL.Process();
        }
    }
    public class Consumer
    {
        public static SingletonPattern SP => SingletonPattern.Instance;
        public static SingletonPatternThreadSafe SPTS => SingletonPatternThreadSafe.Instance;
        public static SingletonPatternWithLazy SPL => SingletonPatternWithLazy.Instance;
    }
    public class SingletonPattern
    {
        private static readonly SingletonPattern _instance = new SingletonPattern();
        public static SingletonPattern Instance => _instance;

        private SingletonPattern()
        {

        }
        public void Process()
        {
            Console.WriteLine("Processed Singleton Pattern - Simple version");
        }
    }

    public class SingletonPatternThreadSafe
    {
        private static SingletonPatternThreadSafe _instance;
        private static readonly Object _lock = new object();

        private SingletonPatternThreadSafe()
        {

        }
        public static SingletonPatternThreadSafe Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SingletonPatternThreadSafe();
                        }
                    }
                }
                return _instance;
            }
        }

        public void Process()
        {
            Console.WriteLine("Processed Singleton Pattern - Thread safe version");
        }
    }

    public class SingletonPatternWithLazy
    {
        private static readonly Lazy<SingletonPatternWithLazy> _instance = new Lazy<SingletonPatternWithLazy>(() => new SingletonPatternWithLazy());
        public static SingletonPatternWithLazy Instance => _instance.Value;

        private SingletonPatternWithLazy()
        {

        }
        public void Process()
        {
            Console.WriteLine("Processed Singleton Pattern - Modern version - Lazy");
        }
    }
}
