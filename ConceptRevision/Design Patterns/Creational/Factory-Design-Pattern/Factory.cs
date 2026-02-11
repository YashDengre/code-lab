using Microsoft.Extensions.DependencyInjection;
using ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern.Interfaces_Objects;
using static ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern.KeyedServicesFactory;

namespace ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern
{
    public class Factory
    {
        // usage of factory:
        private readonly IPaymentFactory factory;
        private readonly IPaymentFactoryOpenClosed factoryOpenClosed;
        private readonly IGenericPaymentFactory genricPaymentFactory;
        private readonly IAttributePaymentFactory attributePaymentFactory;
        private readonly PaymentService keyedPaymentService;
        public Factory(IPaymentFactory _factory, IPaymentFactoryOpenClosed _factoryOpenClosed,
            IGenericPaymentFactory _genricPaymentFactory, IAttributePaymentFactory _attributePaymentFactory, PaymentService _keyedPaymentService)
        {
            factory = _factory;
            factoryOpenClosed = _factoryOpenClosed;
            genricPaymentFactory = _genricPaymentFactory;
            attributePaymentFactory = _attributePaymentFactory;
            keyedPaymentService = _keyedPaymentService;
        }

        public async Task Process(string type, decimal amount)
        {
            //1. PaymentFactory
            var processor = factory.Create(type);
            await processor.ProcessPayment(amount);

            //2. PaymentFactoryOpenClosed - Strategy Implementation (dictionary based)
            processor = factoryOpenClosed.Create(type);
            await processor.ProcessPayment(amount);

            //3. GenericFactory
            processor = type == "upi" ? genricPaymentFactory.GetProcessor<UpiProcessor>() : genricPaymentFactory.GetProcessor<CreditCardProcessor>();
            await processor.ProcessPayment(amount); // In this case consumer should the type and class for the processing.

            //4. AttributeBasedFactory
            // This is same as our string based dictionary (2) where we solved the open closed principle
            //processor = attributePaymentFactory.Get("upi");
            processor = attributePaymentFactory.Get(type);
            await processor.ProcessPayment(amount);

            //5. KeyedService Version
            await keyedPaymentService.PayAsync(type, amount);

        }
    }

    // 2 
    //Factory interface & Its Implementation

    public interface IPaymentFactory
    {
        IPaymentProcessor Create(string paymentType);
    }
    public class PaymentFactory : IPaymentFactory
    {
        private readonly IServiceProvider serviceProvider;
        public PaymentFactory(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }
        public IPaymentProcessor Create(string paymentType)
        {
            return paymentType.ToLower() switch
            {
                "creditcard" => serviceProvider.GetRequiredService<CreditCardProcessor>(),
                "upi" => serviceProvider.GetRequiredService<UpiProcessor>(),
                _ => throw new NotImplementedException("Invalid payment type")
                //breaking open-closed
            };
        }
    }

    // 3
    // Another factory and its implementation to demonstrate the better version which is following open-closed principle: using strategy pattern
    public interface IPaymentFactoryOpenClosed
    {
        IPaymentProcessor Create(string paymentType);
    }

    // Factory which have low dependency of if and switch and little improvment in open-closed

    public class PaymentFactoryOpenClosed : IPaymentFactoryOpenClosed
    {
        private readonly Dictionary<string, IPaymentProcessor> _processors;
        public PaymentFactoryOpenClosed(IEnumerable<IPaymentProcessor> processors)
        {
            _processors = processors.ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase); // for this we have to add Name in payment processor
        }
        public IPaymentProcessor Create(string paymentType)
        {
            if (!_processors.TryGetValue(paymentType, out var processor))
                throw new InvalidOperationException($"Processor not found: {paymentType}");
            return processor;
        }

        //builder.Services.AddScoped<IPaymentProcessorFactory, PaymentProcessorFactory>();
        //builder.Services.AddScoped<PaymentService>();

    }

}
