using ConceptRevision.Design_Patterns.Creational.Abstract_Factory_Design_Pattern.Interfaces_Objetcs;
using MediatR;
using System.Threading.Tasks;

namespace ConceptRevision.Design_Patterns.Behavioral.Observer_Design_Pattern.Enterprise_MediatR_Version
{
    internal class EnterpriseObserverMediatR
    {
        private readonly OrderService _orderService;
        public EnterpriseObserverMediatR(OrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task Process()
        {
            await _orderService.PlaceOrder();
        }
    }

    // 1 Define Domain Event (Observer Notitication)

    public record OrderPlacedEvent(Guid OrderId) : INotification;

    // 2 Domain Entity : Subject (Publisher)

    public class Order
    {
        public readonly List<INotification> _events = [];

        public Guid Id { get; set; } = Guid.NewGuid();

        public IReadOnlyCollection<INotification> Events => _events;
        public void Place()
        {
            Console.WriteLine("Order Placed"!);
            _events.Add(new OrderPlacedEvent(Id));
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }

    // 3 Observer Handlers (Subscribers)

    // Email Observer

    public class SendEmailHandler : INotificationHandler<OrderPlacedEvent>
    {
        public Task Handle(OrderPlacedEvent notification, CancellationToken ct)
        {
            Console.WriteLine($"Email sent for Order: {notification.OrderId}");
            return Task.CompletedTask;
        }
    }

    public class UpdateInventoryHandler : INotificationHandler<OrderPlacedEvent>
    {
        public Task Handle(OrderPlacedEvent notification, CancellationToken ct)
        {
            Console.WriteLine($"Inventory updated for Order: {notification.OrderId}");
            return Task.CompletedTask;
        }
    }

    public class AnalyticsHandler : INotificationHandler<OrderPlacedEvent>
    {
        public Task Handle(OrderPlacedEvent notification, CancellationToken ct)
        {
            Console.WriteLine($"Analytics recorded Order: {notification.OrderId}");
            return Task.CompletedTask;
        }
    }

    // 4 Application Service (Event Dispatcher)

    public class OrderService
    {
        private readonly IMediator _mediator;

        public OrderService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PlaceOrder()
        {
            var order = new Order();
            order.Place();
            foreach(var domainEvent in order.Events)
            {
                await _mediator.Publish(domainEvent);
            }

            order.ClearEvents();
        }
    }

}
