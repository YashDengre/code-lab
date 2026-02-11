namespace ConceptRevision.Design_Patterns.Creational.Abstract_Factory_Design_Pattern.Interfaces_Objetcs
{
    // 1
    // Types and Interfaces - which will we used in actual abstract factory to for object creations
    public interface IPaymentProcessor
    {
        void process(decimal amount);
    }
    public interface ITaxCalculator
    {
        decimal Calculate(decimal amount);
    }
    public interface IInvoiceGenerator
    {
        void Generate(decimal amount);
    }
    // 1.1
    // Implementations of above types based on the region

    // INDIA:
    public class IndiaPaymentProcessor : IPaymentProcessor
    {
        public void process(decimal amount) => Console.WriteLine($"Processing India payement: {amount}");
    }
    public class IndiaTaxCalculator : ITaxCalculator
    {
        public decimal Calculate(decimal amount) => amount * 0.18m;
    }
    public class IndiaInvoiceGenerator : IInvoiceGenerator
    {
        public void Generate(decimal amount) => Console.WriteLine($"India Invoice generated for: {amount}");
    }
    // USA:
    public class USPaymentProcessor : IPaymentProcessor
    {
        public void process(decimal amount) => Console.WriteLine($"Processing US payement: {amount}");
    }
    public class USTaxCalculator : ITaxCalculator
    {
        public decimal Calculate(decimal amount) => amount * 0.07m;
    }
    public class USInvoiceGenerator : IInvoiceGenerator
    {
        public void Generate(decimal amount) => Console.WriteLine($"US Invoice generated for: {amount}");
    }


    // 2
    //Factories
    public interface IPaymentPlatformFactory
    {
        IPaymentProcessor CreateProcessor();
        ITaxCalculator CreateTaxCalculator();
        IInvoiceGenerator CreateInvoiceGenerator();
    }

    // 2.1 
    // Implementations of above factoriers based on the region

    // INDIA:
    public class IndiaPaymentPlatformFactory : IPaymentPlatformFactory
    {
        public IInvoiceGenerator CreateInvoiceGenerator() => new IndiaInvoiceGenerator();

        public IPaymentProcessor CreateProcessor() => new IndiaPaymentProcessor();

        public ITaxCalculator CreateTaxCalculator() => new IndiaTaxCalculator();
    }

    // USA:
    public class USPaymentPlatformFactory : IPaymentPlatformFactory
    {
        public IInvoiceGenerator CreateInvoiceGenerator() => new USInvoiceGenerator();

        public IPaymentProcessor CreateProcessor() => new USPaymentProcessor();

        public ITaxCalculator CreateTaxCalculator() => new USTaxCalculator();
    }
}
