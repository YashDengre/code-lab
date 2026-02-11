namespace ConceptRevision.Design_Patterns.Structural.Facade_Design_Pattern.Interfaces_Objetcs
{

    // 1 Subsystems - interfaces and implementations

    public record Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ItemCount { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
    }
    public interface IFacadePaymentService
    {
        Task<bool> ProcessPaymentAsync(Order order);
    }

    public interface IFacadeInventoryService
    {
        Task<bool> ReserveItemsAsync(Order order);
    }

    public interface IFacadeShippingService
    {
        Task<bool> CreateShipmentAsync(Order order);
    }

    public interface IFacadeFraudService
    {
        Task<bool> ValidateAsync(Order order);
    }

    // Implementation:
    public class FacadePaymentService : IFacadePaymentService
    {
        public async Task<bool> ProcessPaymentAsync(Order order)
        {

            if (order.Amount > 1)
            {
                Console.WriteLine($"Payment is processed for order: {order.OrderId}");
                return await Task.FromResult(true);
            }
            else
            {
                Console.WriteLine($"Payment failed for order: {order.OrderId}");
                return await Task.FromResult(false);
            }
        }
    }
    public class FacadeInventoryService : IFacadeInventoryService
    {
        public async Task<bool> ReserveItemsAsync(Order order)
        {

            if (order.ItemCount < 5 && order.ItemCount > 0)
            {
                Console.WriteLine($"Inventory is reserved for order: {order.OrderId}");
                return await Task.FromResult(true);
            }
            else
            {
                Console.WriteLine($"Inventory is out of stock for order: {order.OrderId}");
                return await Task.FromResult(false);
            }
        }
    }
    public class FacadeShippingService : IFacadeShippingService
    {
        public async Task<bool> CreateShipmentAsync(Order order)
        {

            if (order.CustomerId > 0)
            {
                Console.WriteLine($"Shipping is done for order: {order.OrderId}");
                return await Task.FromResult(true);
            }
            else
            {
                Console.WriteLine($"Shipping is failed for order: {order.OrderId} as customer is not valid.");
                return await Task.FromResult(false);
            }
        }
    }
    public class FacadeFraudService : IFacadeFraudService
    {
        public async Task<bool> ValidateAsync(Order order)
        {
            if (order.Amount < 1000)
            {
                Console.WriteLine($"Fraude is not detected for order: {order.OrderId}");
                return await Task.FromResult(true);
            }
            else
            {
                Console.WriteLine($"Fraude is detected for order: {order.OrderId}");
                return await Task.FromResult(false);
            }
        }
    }
}
