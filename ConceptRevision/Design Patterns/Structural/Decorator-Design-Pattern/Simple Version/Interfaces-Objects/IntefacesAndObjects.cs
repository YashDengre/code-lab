namespace ConceptRevision.Design_Patterns.Structural.Decorator_Design_Pattern.Simple_Version
{
    // 1 Component Interface
    public interface ICoffee
    {
        string GetDescription();
        decimal GetCost();
    }

    // 2 Concrete Componenet - Implementation
    public class SimpleCofee : ICoffee
    {
        public string GetDescription() => "Simple Coffee";
        public decimal GetCost() => 50;
    }
}
