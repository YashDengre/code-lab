using ConceptRevision.Design_Patterns.Behavioral.Chain_Of_Responsibility.Enterprise_Version.Interfaces_Objects;

namespace ConceptRevision.Design_Patterns.Behavioral.Chain_Of_Responsibility.Enterprise_Version
{
    public class EnterpriseCOR
    {
        private readonly OrderPipeline _pipeline;
        public EnterpriseCOR(OrderPipeline pipeline)
        {
            _pipeline = pipeline;
        }

        public async Task Process(decimal amount)
        {
            try
            {
                var context = new OrderContext
                {
                    UserId = "Yash",
                    Amount = amount
                };
                var chain = _pipeline.Build();
                await chain.HandleAsync(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured and COR stopped");
                Console.WriteLine(ex.Message);
            }
        }
        public async Task ManualProcess(decimal amount)
        {
            try
            {
                var context = new OrderContext
                {
                    UserId = "Yash",
                    Amount = amount
                };
                //var validationHandler =  new ValidationHandler();
                //var fraudHandler =  new FraudHandler();
                //var paymentHandler=  new PaymentHandler();
                //var orderCreationHandler =  new OrderCreationHandler();

                //paymentHandler.SetNext(orderCreationHandler);
                //fraudHandler.SetNext(paymentHandler);
                //validationHandler.SetNext(fraudHandler);

                //await validationHandler.HandleAsync(context);

                // by returning the IOrderHandler from SetNext we can directly access the next handler and can see the channing as visually too.
                // validationHandler.SetNext(fraudHandler).SetNext(paymentHandler).SetNext(orderCreationHandler);
                var chain = new ValidationHandler();
                chain.SetNext(new FraudHandler()).SetNext(new PaymentHandler()).SetNext(new OrderCreationHandler());

                // see the difference when earleir we craeted the chain manually and this one
                await chain.HandleAsync(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured and COR stopped");
                Console.WriteLine(ex.Message);
            }
        }
    }

    // 3 Create Pipeline for execution, it is a way to call the COR but not necessory you can direcly build the handlers and chain it and call it in any other usage class.
    public class OrderPipeline
    {
        private readonly IEnumerable<IOrderHandler> _handlers;
        public OrderPipeline(IEnumerable<IOrderHandler> handlers)
        {
            _handlers = handlers;
        }
        public IOrderHandler Build()
        {
            IOrderHandler first = null!;
            IOrderHandler current = null!;
            foreach (var handler in _handlers)
            {
                if (first == null)
                {
                    first = handler;
                    current = handler;
                }
                else
                {
                    current.SetNext(handler);
                    current = handler;
                }
            }
            return first;
        }
    }

    // 2 Handler Contract

    public interface IOrderHandler
    {
        Task HandleAsync(OrderContext context);
        //void SetNext(IOrderHandler next); // Commented this and created below one which returns the IOrderhandler so that we while setting thr 'next' we will have direct access to 'next'
        IOrderHandler SetNext(IOrderHandler next);
    }

    // Base Handler
    public abstract class OrderHandlerBase : IOrderHandler
    {
        private IOrderHandler _next = null!;
        public /*void*/ IOrderHandler SetNext(IOrderHandler next)
        {
            _next = next;
            return next;
        }
        public async Task HandleAsync(OrderContext context)
        {
            await ProcessAsync(context);
            if (_next != null)
            {
                await _next.HandleAsync(context);
            }
        }
        protected abstract Task ProcessAsync(OrderContext context);
    }

    // Concrete Enterprise Handlers

    public class ValidationHandler : OrderHandlerBase
    {
        protected override Task ProcessAsync(OrderContext context)
        {
            if (context.Amount <= 0)
                throw new Exception("Invalid Order");
            Console.WriteLine("[Validation]: validation passed");
            return Task.CompletedTask;
        }
    }

    public class FraudHandler : OrderHandlerBase
    {
        protected override Task ProcessAsync(OrderContext context)
        {
            if (context.Amount > 1000)
            {
                context.IsFraudulent = true;
                throw new Exception("Fraud detected Order");
            }

            Console.WriteLine("[Fraud]: Fraud check completed: No Fraud detected");
            return Task.CompletedTask;
        }
    }

    public class PaymentHandler : OrderHandlerBase
    {
        protected override Task ProcessAsync(OrderContext context)
        {
            context.PaymentSuccess = true;
            Console.WriteLine("Payment Processed");
            return Task.CompletedTask;
        }
    }

    public class OrderCreationHandler : OrderHandlerBase
    {
        protected override Task ProcessAsync(OrderContext context)
        {
            Console.WriteLine("Order Saved to DB");
            return Task.CompletedTask;
        }
    }
}
