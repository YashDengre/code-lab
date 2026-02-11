using ConceptRevision.Design_Patterns.Structural.Adapter_Design_Pattern.LegacySystem;
using ConceptRevision.Design_Patterns.Structural.Adapter_Design_Pattern.ThridPartySDK;
namespace ConceptRevision.Design_Patterns.Structural.Adapter_Design_Pattern
{
    public class AdapterExecutor
    {
        // 3
        // Use in business layer or where you want to call the code of legacy or third party systems

        private readonly IPaymentGateway _paymentGateway;
        public AdapterExecutor(IPaymentGateway paymentGateway)
        {
            _paymentGateway = paymentGateway;
        }
        public async Task<bool> Process(decimal amount)
        {
            return await _paymentGateway.ProcessPaymentAsync(amount, "INR");
        }
    }

    // 2
    // Adapter Impementation

    // Create a adapter which act as a bridge between legacy/third party code and our code.
    // Adpater interface - create a abstract contract so it is easy for DI and future changes
    public interface IPaymentGateway
    {
        Task<bool> ProcessPaymentAsync(decimal amount, string currency);
    }

    // Implementation
    // 2.1 Object Adapter Patter: Where we create object of adaptee in adapter and utilzie the behavior of adaptee through object
    // Third Party
    public class StripePaymentAdapter : IPaymentGateway
    {
        private readonly StripeService _stripeService;
        public StripePaymentAdapter(StripeService stripeService)
        {
            _stripeService = stripeService;
        }
        public async Task<bool> ProcessPaymentAsync(decimal amount, string currency)
        {
            Console.WriteLine("Third Party Stripe Service Execution via Object based adapter:");
            var result = await _stripeService.MakeCharge(amount);
            return result == true;
        }
    }
    // Legacy
    public class LegacyPaymentAdapter : IPaymentGateway
    {
        private readonly LegacyPaymentClient _legacyPaymentClient;
        public LegacyPaymentAdapter(LegacyPaymentClient legacyPaymentClient)
        {
            _legacyPaymentClient = legacyPaymentClient;
        }
        public async Task<bool> ProcessPaymentAsync(decimal amount, string currency)
        {
            Console.WriteLine("Legacy Service Execution via Object based adapter:");
            var result = await _legacyPaymentClient.ExecuteTranscations(amount);
            return result == true;
        }
    }

    // 2.2 Class Adapter Patter: Where we inheri adapter from adaptee and common interface and utilzie the behavior of adaptee through inheritance
    // Third Party
    public class StripePaymentAdapter_Class : StripeService, IPaymentGateway
    {
        public async Task<bool> ProcessPaymentAsync(decimal amount, string currency)
        {
            Console.WriteLine("Third Party Stripe Service Execution via Class based adapter:");
            var result = await MakeCharge(amount);
            return result == true;
        }
    }
    // Legacy
    public class LegacyPaymentAdapter_Class : LegacyPaymentClient, IPaymentGateway
    {
        public async Task<bool> ProcessPaymentAsync(decimal amount, string currency)
        {
            Console.WriteLine("Legacy Service Execution via Class based adapter:");
            var result = await ExecuteTranscations(amount);
            return result == true;
        }
    }
}

