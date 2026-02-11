using ConceptRevision.Design_Patterns.Creational.Builder_Design_Pattern.Enterprise_Version.Interfaces_Objects;

namespace ConceptRevision.Design_Patterns.Creational.Builder_Design_Pattern.Enterpprise_Version
{
    public class EnterpriseBuilder
    {
        public static async Task Process()
        {
            var builder = new OrderBuilder();
            var order = builder.ForCustomer("Cust-1001").AddItem("Laptop").AddItem("Mouse").ApplyDiscount(10).ApplyTax(5).Build();
            Console.WriteLine(order.ToString());
            await Task.CompletedTask;
        }
    }

    public class OrderBuilder
    {
        private string _customerId = null!;
        private List<string> _items = [];
        private decimal _discount;
        private decimal _tax;
        public OrderBuilder ForCustomer(string customerId)
        {
            _customerId = customerId;
            return this;
        }
        public OrderBuilder AddItem(string item)
        {
            _items.Add(item);
            return this;
        }

        public OrderBuilder ApplyDiscount(decimal discount)
        {
            _discount = discount;
            return this;
        }

        public OrderBuilder ApplyTax(decimal tax)
        {
            _tax = tax;
            return this;
        }
        public Order Build()
        {
            if (string.IsNullOrEmpty(_customerId))
                throw new Exception("Customer required");

            if (!_items.Any())
                throw new Exception("Order must have items");

            return new Order(
                _customerId,
                _items,
                _discount,
                _tax,
                DateTime.UtcNow);
        }
    }
}
