using ConceptRevision.Design_Patterns.Behavioral.Observer_Design_Pattern.Enterprise_Version.Interfaces_Objects;

namespace ConceptRevision.Design_Patterns.Behavioral.Observer_Design_Pattern.Enterprise_Version
{
    public class EnterpriseObserver
    {
        public async Task Process()
        {
            var handlers = new List<object>
            {
                new EmailHandler(),
                new InventoryHandler()
            };
            var handler2 = new List<IEventHandler<OrderCreatedEvent>>()
            {
                new EmailHandler(),
                new InventoryHandler()
            };

            var dispatcher = new EventDispatcher(handlers);
            await dispatcher.Publish(new OrderCreatedEvent() { OrderId = Guid.NewGuid() });
        }
    }

    // 3 Observers (Subscribers)

    // Email Handler

    public class EmailHandler : IEventHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent orderCreatedEvent) {

            Console.WriteLine("Sending Email.....");
            return Task.CompletedTask;
        }
    }

    // Inventory Handler
    public class InventoryHandler : IEventHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent orderCreatedEvent)
        {
            Console.WriteLine("Updating Inventory....");
            return Task.CompletedTask;
        }
    }

    // 4 Event Dispatcher (Publisher Engine)

    public class EventDispatcher
    {
        private readonly IEnumerable<object> _handlers;
        public EventDispatcher(IEnumerable<object> handlers)
        {
            _handlers = handlers;
        }
        public async Task Publish<T>(T notification)
        {
            var handlers = _handlers.OfType<IEventHandler<T>>();
            foreach (var item in handlers)
            {
                await item.Handle(notification);
            } 
        }
    }
}
