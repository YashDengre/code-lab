using ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern.Interfaces_Objects;

namespace ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern
{
    public interface IAttributePaymentFactory
    {
        IPaymentProcessor Get(string type);
    }

    internal class AttributePaymentFactory : IAttributePaymentFactory
    {
        private readonly Dictionary<string, IPaymentProcessor> _processors;
        public AttributePaymentFactory(IEnumerable<IPaymentProcessor> processors)
        {
            _processors = new Dictionary<string, IPaymentProcessor>();
            foreach (IPaymentProcessor processor in processors)
            {
                var attr = processor.GetType().GetCustomAttributes(typeof(PaymentTypeAttribute), false).FirstOrDefault() as PaymentTypeAttribute;
                if (attr != null)
                {
                    _processors[attr.Name] = processor;
                }
            }
        }
        public IPaymentProcessor Get(string type)
        {
            return _processors[type];
        }
    }

}
