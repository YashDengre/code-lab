using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern.KeyedServicesFactory;

namespace ConceptRevision.Design_Patterns.Structural.Additional.Decorator_Proxy_Comaparison
{
    public class DecoratorAndProxyComparision
    {
        public static async Task Process()
        {
            Console.WriteLine("Decorator (Behavior Enhancement):");
            IDP_PaymentService service = new DP_LoggingPaymentDecorator(new DP_LoggingIPaymentService());
            await service.ProcessAsync(100);
            // 🧠 Why this is Decorator?
            // Because: 👉 purpose = enhance functionality
            // You can stack: RetryDecorator, ValidationDecorator, MetricsDecorator and it becomes Feature pipeline
            Console.WriteLine("\nWhy this is Decorator?\nBecause: purpose = enhance functionality\nYou can stack: RetryDecorator, ValidationDecorator, MetricsDecorator and it becomes Feature pipeline");

            Console.WriteLine("\nProxy (Access Control / Lifecycle):");
            service = new DP_PaymentServiceProxy("Admin");
            await service.ProcessAsync(100);
            Console.WriteLine("\nSame structure but intent is different\r\nScenario: Payment service is remote API, expensive to initiaize, requires auth: proxy control access.");

            Console.WriteLine("\nReal Enterprise Analogy:\nDecorator: ASP.NET Middleware:=>Logging → Validation → Retry → Metrics\nProxy: HttpClient SDK=>AuthProxy → Remote API");
            Console.WriteLine("\nThe Deepest Insight:\nYou can write identical code: Wrapper → _inner.Method()\n\tand it can be:\n\t\tDecorator\n\t\tProxy\n\t\tAdapter\n\t\tFacade\n\tbecause… Patterns are defined by intent, not syntax");

        }
    }

    // 1. Take Payment Processing Example
    // We wil implement Decorator and Proxy verions
    // Both will implement same interface, wrap same service and log calls.
    // But the reason why they exist willbe different.

    // Base Service Common for both
    // Interface
    public interface IDP_PaymentService
    {
        Task ProcessAsync(decimal amount);
    }

    // Implementaiton 
    public class DP_LoggingIPaymentService : IDP_PaymentService
    {
        public async Task ProcessAsync(decimal amount)
        {
            Console.WriteLine($"Processing payment {amount}");
            await Task.Delay(200);
        }
    }

    // 2. Decorator (Behavior Enhancement)
    // intent: Add extra busniess/technical behavior
    // Real Enterprise scenarios: Logging, Retry, Validation, Metrics : Layrable features.
    // Created the concrete version just for the example but in real world we should create with abstract class  as base decorator but using this just for comparison

    public class DP_LoggingPaymentDecorator : IDP_PaymentService
    {
        private readonly IDP_PaymentService _inner;

        public DP_LoggingPaymentDecorator(IDP_PaymentService inner)
        {
            _inner = inner;
        }

        public async Task ProcessAsync(decimal amount)
        {
            Console.WriteLine("Decorator: Before payment");

            await _inner.ProcessAsync(amount);

            Console.WriteLine("Decorator: After payment");
        }
    }

    // 3. Proxy (Access Control / Lifecycle)
    // Same structure but intent is different
    // Scenario: Payment service is remote API, expensive to initiaize, requires auth: proxy control access.

    public class DP_PaymentServiceProxy: IDP_PaymentService
    {
        private DP_LoggingIPaymentService _realService;
        private readonly string _role;

        public DP_PaymentServiceProxy(string role)
        {
            _role = role;
        }

        public async Task ProcessAsync(decimal amount)
        {
            Console.WriteLine("Proxy: checking permission");

            if (_role != "Admin")
                throw new UnauthorizedAccessException();

            if (_realService == null)
            {
                Console.WriteLine("Proxy: Lazy loading real service");
                _realService = new DP_LoggingIPaymentService();
            }

            await _realService.ProcessAsync(amount);
        }
    }
}
