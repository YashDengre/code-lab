namespace ConceptRevision.Design_Patterns.Structural.Decorator_Design_Pattern.Simple_Version
{

    // 5 Usage
    public class SimpleDecorator
    {
        private readonly ICoffee coffee;
        public SimpleDecorator(ICoffee _coffee)
        {
            coffee = _coffee ?? new SimpleCofee();
        }

        public async Task Process()
        {
            Console.WriteLine(coffee.GetDescription());
            Console.WriteLine(coffee.GetCost());
        }
    }


    // 3 Base Decorator
    public abstract class CoffeeDecorator : ICoffee
    {
        protected readonly ICoffee _coffee;
        public CoffeeDecorator(ICoffee coffee)
        {
            _coffee = coffee;
        }
        public virtual string GetDescription() => _coffee.GetDescription();
        public virtual decimal GetCost() => _coffee.GetCost();
    }

    // 4 Concreate Decorators

    public class MilkDecorator : CoffeeDecorator
    {
        public MilkDecorator(ICoffee _coffee) : base(_coffee) { }
        public override string GetDescription() => _coffee.GetDescription() + ", with Milk";
        public override decimal GetCost() => _coffee.GetCost() + 20;
    }

    public class SugarDecorator : CoffeeDecorator
    {
        public SugarDecorator(ICoffee _coffee) : base(_coffee) { }
        public override string GetDescription() => _coffee.GetDescription() + ", with Sugar";
        public override decimal GetCost() => _coffee.GetCost() + 10;
    }
}
