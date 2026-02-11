using ConceptRevision.Design_Patterns.Creational.Builder_Design_Pattern.Simple_Version.Interfaces_Objects;

namespace ConceptRevision.Design_Patterns.Creational.Builder_Design_Pattern.Simple_Version
{
    public class SimpleBuilder
    {
        public static async Task Process()
        {
            var builder = new ComputerBuilder();
            var director = new ComputerDirector();
            var gamingPC = director.BuildGamingPC(builder);
            Console.WriteLine(gamingPC);
            await Task.CompletedTask;
        }
    }

    // 3 Concrete Builder

    public class ComputerBuilder : IComputerBuilder
    {
        private Computer _computer = new Computer();

        public Computer Build()
        {
            return _computer;
        }

        public void SetCPU(string cpu)
        {
            _computer.CPU = cpu;
        }

        public void SetRAM(string ram)
        {
            _computer.RAM = ram;
        }

        public void SetStorage(string storage)
        {
            _computer.Storage = storage;
        }
    }

    // 4 Director (Optional but Classic)

    public class ComputerDirector
    {
        public Computer BuildGamingPC(IComputerBuilder builder)
        {
            builder.SetCPU("i9");
            builder.SetRAM("32GB");
            builder.SetStorage("2TB SSD");
            return builder.Build();
        }
    }
}
