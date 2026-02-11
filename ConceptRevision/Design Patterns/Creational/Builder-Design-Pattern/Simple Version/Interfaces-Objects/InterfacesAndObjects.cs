namespace ConceptRevision.Design_Patterns.Creational.Builder_Design_Pattern.Simple_Version.Interfaces_Objects
{
    // 1
    // Create Type and Objects to used in builder

    public class Computer
    {
        public string CPU { get; set; } = null!;
        public string RAM { get; set; } = null!;
        public string Storage { get; set; } = null!;
        public override string ToString()
        {
            return $"CPU: {CPU}, RAM: {RAM}, Storage: {Storage}";
        }
    }

    // 2
    // Build Interface

    public interface IComputerBuilder
    {
        void SetCPU(string cpu);
        void SetRAM(string ram);
        void SetStorage(string storage);
        Computer Build();
    }

}
