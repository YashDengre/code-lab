using Microsoft.Extensions.DependencyInjection;
using ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern.Interfaces_Objects;

namespace ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern
{
    public class KeyedServicesFactory
    {
        // In .NET 8 and after that, many people prefer to use Keyed Servoces of object creations:

        // In DI Registration we need to do in such a way

        //builder.Services.AddKeyedScoped<IPaymentProcessor, UpiProcessor>("upi");
        //builder.Services.AddKeyedScoped<IPaymentProcessor, CreditCardProcessor>("card");

        // Keyed services internally uses same as strategy which we used in (2) but difference is : it is built in object resolution mechanism which is an underlying concept in .NET
        public class PaymentService
        {
            private readonly IServiceProvider _provider;

            public PaymentService(IServiceProvider provider)
            {
                _provider = provider;
            }

            public async Task PayAsync(string type, decimal amount)
            {
                Console.WriteLine("Keyed Services Version:");
                var processor = _provider
                    .GetRequiredKeyedService<IPaymentProcessor>(type);

                await processor.ProcessPayment(amount);
            }
        }


    }
}
