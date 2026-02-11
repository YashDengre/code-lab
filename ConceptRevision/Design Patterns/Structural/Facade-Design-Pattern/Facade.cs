using ConceptRevision.Design_Patterns.Structural.Facade_Design_Pattern.Interfaces_Objetcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Design_Patterns.Structural.Facade_Design_Pattern
{
    public class Facade
    {
        private readonly IOrderFacade _orderFacade;
        public Facade(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        public async Task Process(int productId, int customerId, decimal amount, int itemCount)
        {
            var order = new Order() { Amount = amount, CustomerId = customerId, ItemCount = itemCount, OrderId = Random.Shared.Next(), ProductId = productId };
            var isOrderPlaced = await _orderFacade.PlaceOrderAsync(order);
            if (isOrderPlaced)
            {
                Console.WriteLine("Order Placed Successfull!");
            }
            else
            {
                Console.WriteLine("Order placement failed");
            }
        }
    }

    // 2 
    // Facade Implementation

    public interface IOrderFacade
    {
        Task<bool> PlaceOrderAsync(Order order);
    }

    public class OrderProcessingFacade : IOrderFacade
    {
        private readonly IFacadePaymentService _facadePaymentService;
        private readonly IFacadeInventoryService _facadeInventoryService;
        private readonly IFacadeShippingService _facadeShippingService;
        private readonly IFacadeFraudService _facadeFraudService;

        public OrderProcessingFacade(IFacadePaymentService facadePaymentService, IFacadeInventoryService facadeInventoryService,
                                    IFacadeShippingService facadeShippingService, IFacadeFraudService facadeFraudService)
        {
            _facadePaymentService = facadePaymentService;
            _facadeInventoryService = facadeInventoryService;
            _facadeShippingService = facadeShippingService;
            _facadeFraudService = facadeFraudService;
        }

        public async Task<bool> PlaceOrderAsync(Order order)
        {
            if (!await _facadeFraudService.ValidateAsync(order))
            {
                Console.WriteLine("Fraud detection ran and validation failed!");
                return false;
            }
            if (!await _facadeInventoryService.ReserveItemsAsync(order))
            {
                Console.WriteLine("Inventory service ran and validation failed!");
                return false;
            }
            if (!await _facadePaymentService.ProcessPaymentAsync(order))
            {
                Console.WriteLine("Payment service ran and validation failed!");
                return false;
            }
            if (!await _facadeShippingService.CreateShipmentAsync(order))
            {
                Console.WriteLine("Shippiing service ran and validation failed!");
                return false;
            }
            return true;
        }
    }
}
