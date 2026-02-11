using ConceptRevision.Design_Patterns.Creational.Builder_Design_Pattern.Enterprise_Version.Interfaces_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConceptRevision.Design_Patterns.Creational.Builder_Design_Pattern.Enterpprise_Version
{
    public class EnterpriseBuilderV2
    {
        public static async Task Process()
        {
            var builder = new StandardOrderV2Builder();
            var service = new OrderV2ProcessingService();

            var order = service.CreateStandardOrder(builder, "Cust-101", new List<string> { "Laptop", "Mobile", "Keyboard", "Mouse" }, "CreditCard");

            Console.WriteLine($"Order Total: {order.Total}");
            await Task.CompletedTask;
        }
    }

    // 2. Builder Interface: // we can replace this with asbtract class as well and provide some default behavior to the methods like Build or any other depends on the scenario

    public interface IOrderBuilderV2
    {
        void SetCustomer(string customerId);
        void AddItems(List<string> items);
        void CalculatePricing();
        void ApplyDiscount();
        void CalculateTax();
        void ConfigureShipping();
        void ConfigurePayment(string method);
        OrderV2 Build();
    }

    // 3. Concrete Enterprise builder

    public class StandardOrderV2Builder : IOrderBuilderV2
    {
        private string _customerId = null!;
        private List<string> _items = [];
        private decimal _subTotal;
        private decimal _tax;
        private decimal _discount;
        private decimal _shipping;
        private string _paymentMethod = null!;

        public void SetCustomer(string customerId)
        {
            _customerId = customerId;
        }
        public void AddItems(List<string> items)
        {
            _items.AddRange(items);
        }
        public void CalculatePricing()
        {
            _subTotal = _items.Count * 100; // Simulating price service;
        }
        public void ApplyDiscount()
        {
            _discount = _subTotal > 500 ? 50 : 0;
        }
        public void CalculateTax()
        {
            _tax = (_subTotal - _discount) * 0.18m;
        }
        public void ConfigureShipping()
        {
            _shipping = 40;
        }
        public void ConfigurePayment(string method)
        {
            _paymentMethod = method;
        }

        public OrderV2 Build()
        {
            var total = _subTotal - _discount + _tax + _shipping;

            return new OrderV2(
                          Guid.NewGuid(),
                          _customerId,
                          _items,
                          _subTotal,
                          _tax,
                          _discount,
                          _shipping,
                          total,
                          _paymentMethod,
                          DateTime.UtcNow);
        }
    }

    // 4. Director (Enterprise Orchestration Layer) -  to separtely call the builder and build the object 
    // In enterprise systems this is usually:
    //- Application Service
    //- Domain Service
    //- Workflow Handler
    //- Orchestration Layer

    public class OrderV2ProcessingService
    {
        public OrderV2 CreateStandardOrder(IOrderBuilderV2 builder, string customerId, List<string> items, string paymentMethod)
        {
            builder.SetCustomer(customerId);
            builder.AddItems(items);
            builder.CalculatePricing();
            builder.ApplyDiscount();
            builder.CalculateTax();
            builder.ConfigureShipping();
            builder.ConfigurePayment(paymentMethod);
            return builder.Build();
        }
    }
}
