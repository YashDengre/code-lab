namespace ConceptRevision.Design_Patterns.Structural.Proxy_Design_Pattern.Enterprise_Version.Interfaces_Objects
{
    // 1
    // Interface of a real service
    public interface IPaymentGateway
    {
        Task<bool> ProcessAsync(decimal amount, int orderId);
    }

    //2 
    // Implementation of Real Service
    public class StripeGateway : IPaymentGateway
    {
        public async Task<bool> ProcessAsync(decimal amount, int orderId)
        {
            Console.WriteLine("Calling Stripe API...");
            await Task.Delay(500);
            Console.WriteLine($"Payment Processed for order: {orderId}");
            return true;
        }
    }
}
