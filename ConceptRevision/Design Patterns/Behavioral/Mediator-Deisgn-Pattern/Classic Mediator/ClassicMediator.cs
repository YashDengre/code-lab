namespace ConceptRevision.Design_Patterns.Behavioral.Mediator_Deisgn_Pattern.Classic_Mediator
{


    //5 Usage:
    public class ClassicMediator
    {
        public static async Task Process()
        {
            var mediator = new ClassicOrderMediator();
            var payment = new PaymentComponent(mediator);
            var inventory = new InventoryComponent(mediator);
            var shipping = new ShippingComponent(mediator);
            var notification = new NotificationComponent(mediator);
            var fraud = new FraudComponent(mediator);

            mediator.Payment = payment;
            mediator.Inventory = inventory;
            mediator.Shipping = shipping;
            mediator.Notification = notification;
            mediator.Fraud = fraud;
            await payment.ProcessPaymentAsync(101);
        }
    }

    // Problem: 
    // Suppose we have components like A,B,C,D,E... etc
    // A->, B->C, C->A, A->D, D->B, C->E, E->B // Objects are communicating with each other directly - it will make it complex and dependent etc

    // In order to avoid this situation we create a intermediate component knows as Mediator which enables these components to communicate without directly knowing the other objects.

    // 1 Mediator Contract

    public interface IClassciOrderMediator
    {
        Task NotifyAsync(string eventType, object data);
    }

    // 2 Base Component: It will act as a base component for all the other component, example: Component A is dervied from this base..

    public abstract class BaseOrderComponenet
    {

        protected IClassciOrderMediator _mediator;
        protected BaseOrderComponenet(IClassciOrderMediator mediato)
        {
            _mediator = mediato;
        }
    }

    // 3. Concrete Components:

    public class PaymentComponent : BaseOrderComponenet
    {
        public PaymentComponent(IClassciOrderMediator mediator) : base(mediator) { }

        public async Task ProcessPaymentAsync(int orderId)
        {
            Console.WriteLine("Payment processed!");
            await _mediator.NotifyAsync("PaymentCompleted", orderId);
        }
    }

    public class InventoryComponent : BaseOrderComponenet
    {
        public InventoryComponent(IClassciOrderMediator mediator) : base(mediator) { }
        public async Task ReserveAsync(int orderId)
        {
            Console.WriteLine("Inventory reserved!");
            await _mediator.NotifyAsync("InventoryReserved", orderId);
        }
    }

    public class ShippingComponent : BaseOrderComponenet
    {
        public ShippingComponent(IClassciOrderMediator mediator) : base(mediator) { }
        public async Task CreateShipmentAsync(int orderId)
        {
            Console.WriteLine("Shipment Created!");
            await Task.CompletedTask;
        }
    }

    public class NotificationComponent : BaseOrderComponenet
    {
        public NotificationComponent(IClassciOrderMediator mediator) : base(mediator) { }
        public async Task SendAsync(string message)
        {
            Console.WriteLine($"Notification: {message}!");
            await Task.CompletedTask;
        }
    }

    public class FraudComponent : BaseOrderComponenet
    {
        public FraudComponent(IClassciOrderMediator mediator) : base(mediator) { }
        public async Task ValidateAsync(int orderId)
        {
            Console.WriteLine("Fraud check completed");
            await _mediator.NotifyAsync("FraudValidated", orderId);
        }
    }

    // 4 Enterprise Mediator Implementation

    public class ClassicOrderMediator : IClassciOrderMediator
    {
        public PaymentComponent Payment { get; set; } = null!;
        public InventoryComponent Inventory { get; set; } = null!;
        public ShippingComponent Shipping { get; set; } = null!;
        public NotificationComponent Notification { get; set; } = null!;
        public FraudComponent Fraud { get; set; } = null!;
        public async Task NotifyAsync(string eventType, object data)
        {
            var orderId = (int)data;
            switch (eventType)
            {
                case "PaymentCompleted":
                    await Fraud.ValidateAsync(orderId); break;
                case "FraudValidated":
                    await Inventory.ReserveAsync(orderId); break;
                case "InventoryReserved":
                    await Shipping.CreateShipmentAsync(orderId);
                    await Notification.SendAsync("Order Shipped");
                    break;
            }
        }
    }
}
