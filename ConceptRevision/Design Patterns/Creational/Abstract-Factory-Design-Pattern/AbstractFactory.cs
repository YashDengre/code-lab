using ConceptRevision.Design_Patterns.Creational.Abstract_Factory_Design_Pattern.Interfaces_Objetcs;

namespace ConceptRevision.Design_Patterns.Creational.Abstract_Factory_Design_Pattern
{
    // 5 Business logic where abstract factory canbe used to get the factory which returns the objects
    public class AbstractFactory
    {
        private readonly IPaymentFactoryResolver paymentFactoryResolver;
        public AbstractFactory(IPaymentFactoryResolver _paymentFactoryResolver)
        {
            paymentFactoryResolver = _paymentFactoryResolver;
        }

        public void Process(string region, decimal amount)
        {
            var factory = paymentFactoryResolver.Resolve(region);

            var processor = factory.CreateProcessor();
            var taxCalculator = factory.CreateTaxCalculator();
            var invoiceGenerator = factory.CreateInvoiceGenerator();

            var tax = taxCalculator.Calculate(amount);
            processor.process(amount + tax);
            invoiceGenerator.Generate(amount + tax);
        }
    }

    // 4
    // Factory Resolver

    public interface IPaymentFactoryResolver
    {
        IPaymentPlatformFactory Resolve(string region);
    }

    public class PaymentFactoryResolver : IPaymentFactoryResolver
    {
        private readonly IDictionary<string, IPaymentPlatformFactory> _factories;
        public PaymentFactoryResolver(IEnumerable<IPaymentPlatformFactory> factories)
        {
            _factories = factories.ToDictionary(f => f.GetType().Name.Replace("PaymentPlatformFactory", "").ToLower());
        }
        public IPaymentPlatformFactory Resolve(string region)
        {
            return _factories[region.ToLower()];
        }
    }
}
