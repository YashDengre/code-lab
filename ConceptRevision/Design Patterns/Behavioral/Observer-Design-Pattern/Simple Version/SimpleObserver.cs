

using ConceptRevision.Design_Patterns.Behavioral.Observer_Design_Pattern.Simple_Version.Interfaces_Objects;

namespace ConceptRevision.Design_Patterns.Behavioral.Observer_Design_Pattern.Simple_Version
{
    public class SimpleObserver
    {
        // 4 Usages:

        public Task Process()
        {
            var orderService =  new OrderService();
            orderService.Subscribe(new EmailService());
            orderService.Subscribe(new InventoryService());
            orderService.PlaceOrder();
            return Task.CompletedTask;

        }
    }

    // 2 Subject -  Publisher

    public class OrderService
    {
        private readonly List<Interfaces_Objects.IObserver> _observers = [];

        public void Subscribe(Interfaces_Objects.IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscrive(Interfaces_Objects.IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void PlaceOrder()
        {
            Console.WriteLine("Order Placed");
            Notify("New Order Created");
        }
        public void Notify(string message)
        {
            Console.WriteLine("Notification send to all the subscribers");
            _observers.ForEach(o => { o.Update(message); });
        }
    }

    // 3 Observers

    public class EmailService : IObserver
    {
        public void Update(string message)
        {
            Console.WriteLine($"Email Sent: {message}");
        }
    }
    public class InventoryService : IObserver
    {
        public void Update(string message)
        {
            Console.WriteLine($"Inventory Updated: {message}");
        }
    }
}