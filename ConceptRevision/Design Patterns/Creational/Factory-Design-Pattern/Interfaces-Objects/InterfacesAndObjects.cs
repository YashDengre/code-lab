namespace ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern.Interfaces_Objects
{
    // 1
    // Create - Dependencies and Objects: Factory will take care of creation of these objects

    public interface IPaymentProcessor
    {
        string Name { get; }
        Task ProcessPayment(decimal amount);
    }
    public class CreditCardProcessor : IPaymentProcessor
    {
        public string Name => "creditcard";

        public Task ProcessPayment(decimal amount)
        {

            Console.WriteLine($"Processing Credit Card: {amount}");
            return Task.CompletedTask;
        }
    }

    public class UpiProcessor : IPaymentProcessor
    {
        public string Name => "upi";

        public Task ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing UPI: {amount}");

            return Task.CompletedTask;
        }
    }

    // 3: For Attribute version

    [AttributeUsage(AttributeTargets.Class)]
    public class PaymentTypeAttribute : Attribute
    {
        public string Name { get; set; }
        public PaymentTypeAttribute(string name)
        {
            Name = name;
        }
    }

    [PaymentType("upi")]
    public class AttributeUpiProcessor : IPaymentProcessor
    {
        //not required this but we are suing same IPaymentProcessor to show case this example.hence we have just implemented this property.
        public string Name => "upi-at";
        public Task ProcessPayment(decimal amount)
        {
            Console.WriteLine($"UPI Payment: {amount}");
            return Task.CompletedTask;
        }
    }

    [PaymentType("creditcard")]
    public class AttributeCreditCardProcessor : IPaymentProcessor
    {
        //not required this but we are suing same IPaymentProcessor to show case this example.hence we have just implemented this property.
        public string Name => "creditcard-at";

        public Task ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Card Payment: {amount}");
            return Task.CompletedTask;
        }
    }
}
