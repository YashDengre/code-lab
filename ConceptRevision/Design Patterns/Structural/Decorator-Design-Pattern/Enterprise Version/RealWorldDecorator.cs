using ConceptRevision.Design_Patterns.Structural.Decorator_Design_Pattern.EnterpriseVersion.Interfaces_Objects;

namespace ConceptRevision.Design_Patterns.Structural.Decorator_Design_Pattern.EnterpriseVersion
{
    public class RealWorldDecorator
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerService baseService = new CustomerService();
        public RealWorldDecorator(ICustomerService customerService)
        {
            _customerService = customerService ?? baseService;
        }
        public async Task Process(int id)
        {
            try
            {
                var result = await _customerService.GetAsync(id);
                Console.WriteLine($"Id: {result.Id} | Name: {result.Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured and Handled in catch block!");
                Console.WriteLine(ex.Message);
            }
        }
    }

    // 3 Base Decorator
    // Base Decorator is required as it provide the base behavior of the inital object (in this case service so that base behavior is available for consumer with decorator as well)
    // And we are marking original methods as virtaul so that i can be modify by the decorator later

    public abstract class CustomerServiceDecorator : ICustomerService
    {
        protected readonly ICustomerService _inner;
        protected CustomerServiceDecorator(ICustomerService customerService)
        {
            _inner = customerService;
        }
        public virtual Task<Customer> GetAsync(int id) => _inner.GetAsync(id);
    }

    // 4 
    // Logging Decorator - A behavior we are adding to customer service class, it can be anything
    public class LoggingCustomerService : CustomerServiceDecorator
    {
        public LoggingCustomerService(ICustomerService _inner) : base(_inner) { }

        public override async Task<Customer> GetAsync(int id)
        {
            Console.WriteLine($"[LOG] Fetching Customer {id}");
            var result = await _inner.GetAsync(id);
            Console.WriteLine($"[LOG] Done!");
            return result;
        }
    }

    // 5 
    // Logging Decorator - A behavior we are adding to customer service class, it can be anything
    public class ValidationCustomerService : CustomerServiceDecorator
    {
        public ValidationCustomerService(ICustomerService _inner) : base(_inner) { }
        public override async Task<Customer> GetAsync(int id)
        {
            Console.WriteLine($"[Validation] Validation started for id: {id}");
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Id");
            }
            Console.WriteLine($"[Validation] Validation Completed!");
            return await _inner.GetAsync(id);
        }
    }
}
