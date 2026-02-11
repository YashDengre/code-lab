using Microsoft.Extensions.DependencyInjection;
using ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern.Interfaces_Objects;
namespace ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern
{
    public interface IGenericPaymentFactory
    {
        T GetProcessor<T>() where T : IPaymentProcessor;
    }
    public class GenericPaymentFactory : IGenericPaymentFactory
    {
        private readonly IServiceProvider serviceProvider;

        public GenericPaymentFactory(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }
        public T GetProcessor<T>() where T : IPaymentProcessor
        {
            return serviceProvider.GetRequiredService<T>();
        }
    }
}
