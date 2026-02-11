namespace ConceptRevision.Design_Patterns.Structural.Decorator_Design_Pattern.EnterpriseVersion.Interfaces_Objects
{
    // 1 Behavior to be decorate

    // 1.1 Model
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
    // 1.2 Service
    public interface ICustomerService
    {
        Task<Customer> GetAsync(int id);
    }
    
    // 2 Core Service Implementation : Initial
    public class CustomerService : ICustomerService
    {
        public Task<Customer> GetAsync(int id)
        {
            Console.WriteLine("Fetching from DB.....");
            return Task.FromResult(new Customer { Id = id, Name = "Yash Dengre" });
        }
    }

}
