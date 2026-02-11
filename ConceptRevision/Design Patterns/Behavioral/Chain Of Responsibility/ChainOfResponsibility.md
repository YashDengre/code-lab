### 🧩 Chain of Responsibility (CoR)
✅ What is this pattern?

A behavioral pattern where:

- 👉 a request passes through a chain of handlers
- 👉 each handler decides:

    - handle it ✅
    - partially handle it ⚙️
    - pass to next ➡️

and sender doesn’t know who will process it.

So instead of: if...else...else...else...

you create a processing pipeline.

### 🎯 Why does this pattern exist?

Because real enterprise systems often need:

- sequential processing
- layered validation
- middleware-style execution
- dynamic pipelines
- separation of concerns

Without CoR you get:

- ❌ giant service classes
- ❌ nested conditions
- ❌ tight coupling
- ❌ difficult extensibility

### 🚨 Problem it solves

Imagine:

    Request comes in →
    validate →
    authorize →
    log →
    transform →
    execute →
    audit →
    notify


Without CoR:

    MegaService.Process()
    {
        Validate();
        Authorize();
        Log();
        Transform();
        Execute();
    }


Now:

- adding step = modify class ❌
- testing individual step = hard ❌
- reordering = messy ❌

🧠 Core Idea

Each handler:

knows only:
- its work
- next handler

NOT whole pipeline.

### 🏗️ Structure
    Client
       ↓
    Handler1 → Handler2 → Handler3 → Handler4

### 🧪 SIMPLE EXAMPLE (C#)
🎯 Example: Request Validation Pipeline
Step 1 — Handler base

    public abstract class Handler
        {
            protected Handler _next;

            public Handler SetNext(Handler next)
            {
                _next = next;
                return next;
            }

            public virtual void Handle(Request request)
            {
                _next?.Handle(request);
            }
        }

Step 2 — Concrete handlers
Logging

    public class LoggingHandler : Handler
    {
        public override void Handle(Request request)
        {
            Console.WriteLine("Logging request");
            base.Handle(request);
        }
    }

Validation

    public class ValidationHandler : Handler
    {
        public override void Handle(Request request)
        {
            if(string.IsNullOrEmpty(request.Name))
                throw new Exception("Invalid request");

            Console.WriteLine("Validation passed");
            base.Handle(request);
        }
    }

Business Execution

    public class ExecutionHandler : Handler
    {
        public override void Handle(Request request)
        {
            Console.WriteLine("Executing business logic");
        }
    }

Step 3 — Build Chain

    var logging = new LoggingHandler();
    var validation = new ValidationHandler();
    var execution = new ExecutionHandler();

    logging.SetNext(validation)
           .SetNext(execution);

    logging.Handle(new Request { Name = "Yash" });

✅ What happened?

Client called only:

    logging.Handle()

Pipeline executed automatically.


### 🏢 ENTERPRISE EXAMPLE (REAL .NET WORLD)
🔥 ASP.NET Core Middleware Pipeline

You are literally using CoR daily.

Request Flow:

    UseAuthentication
    UseAuthorization
    UseLogging
    UseExceptionHandler
    UseEndpoints


Each middleware:

- receives request
- can stop chain
- or call next()

Middleware example

    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("Before");

            await _next(context); // pass to next

            Console.WriteLine("After");
        }
    }


THIS is Chain of Responsibility.

🏢 Another Enterprise Example (Very Real)

#### MediatR Pipeline Behaviors
- ValidationBehavior
- LoggingBehavior
- TransactionBehavior
- RetryBehavior
- Actual Handler

Every request passes through chain.

#### 🌍 Real World Use Cases (you have seen these)
Backend (.NET)

- Middleware pipeline
- MediatR pipeline
- Validation pipelines
- Authorization policies
- Message processing
- Background worker processing
- Exception filters

Angular

_ HTTP interceptors
_ Guards
_ Pipes

Infrastructure

- Payment processing
- Rule engines
- Workflow systems
- Fraud detection chains

#### ⚠️ Common Mistakes
- ❌ giant handler doing everything: Kills purpose.
- ❌ handler aware of full chain: Breaks decoupling.
- ❌ using if/else inside single handler: That’s not CoR.
- ❌ mixing orchestration + processing: Facade != Chain.

🧠 Very Important Line

Chain of Responsibility creates a processing pipeline where each handler processes or forwards the request, decoupling sender from receiver and enabling flexible execution flow.

🧩 Quick Comparison (since we discussed many patterns)

| Pattern	| Focus|
|------|-------|
|Decorator|	add behavior to object|
|Proxy|	control access|
|Facade	|simplify subsystem|
|Chain|	sequential processing pipeline|





### 🏢 Enterprise Level Chain of Responsibility Example

🎯 Scenario — Enterprise Order Processing Pipeline

Very real enterprise flow:

	Order API →
	Validation →
	Fraud Check →
	Inventory Check →
	Payment Processing →
	Order Creation →
	Event Publishing →
	Audit Logging


Each step:

- independent
- reusable
- reorderable
- testable
- injectable

THIS is where CoR shines

🧠 Why Enterprise Uses CoR Here

Without CoR:

	OrderService.ProcessOrder()
	{
	   Validate();
	   FraudCheck();
	   Inventory();
	   Payment();
	   Save();
	   Publish();
	}


Problems:

- mega service ❌
- cannot add/remove steps easily ❌
- hard testing ❌
- violates SRP ❌
- runtime pipeline impossible ❌

Refer the Exmaple: 🏗️ Enterprise Architecture Design

### Reqeust Model:

    public class OrderContext
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public bool IsFraudulent { get; set; }
        public bool PaymentSuccess { get; set; }
    }

### 🔗 Handler Contract

Enterprise design normally uses async.

    public interface IOrderHandler
    {
        Task HandleAsync(OrderContext context);
        void SetNext(IOrderHandler next);
    }

### 🧱 Base Handler

    public abstract class OrderHandlerBase : IOrderHandler
    {
        private IOrderHandler _next;

        public void SetNext(IOrderHandler next)
        {
            _next = next;
        }

        public async Task HandleAsync(OrderContext context)
        {
            await ProcessAsync(context);

            if (_next != null)
                await _next.HandleAsync(context);
        }

        protected abstract Task ProcessAsync(OrderContext context);
    }

### 🔹 Concrete Enterprise Handlers

#### ✅ Validation Handler
    public class ValidationHandler : OrderHandlerBase
    {
        protected override Task ProcessAsync(OrderContext context)
        {
            if (context.Amount <= 0)
                throw new Exception("Invalid Order");

            Console.WriteLine("Validation Passed");
            return Task.CompletedTask;
        }
    }

#### 🔐 Fraud Handler

    public class FraudCheckHandler : OrderHandlerBase
    {
        protected override Task ProcessAsync(OrderContext context)
        {
            if (context.Amount > 100000)
                context.IsFraudulent = true;

            Console.WriteLine("Fraud Check Completed");
            return Task.CompletedTask;
        }
    }

#### 💳 Payment Handler

    public class PaymentHandler : OrderHandlerBase
    {
        protected override Task ProcessAsync(OrderContext context)
        {
            context.PaymentSuccess = true;
            Console.WriteLine("Payment Processed");
            return Task.CompletedTask;
        }
    }

#### 📦 Order Creation Handler

    public class OrderCreationHandler : OrderHandlerBase
    {
        protected override Task ProcessAsync(OrderContext context)
        {
            Console.WriteLine("Order Saved to DB");
            return Task.CompletedTask;
        }
    }

#### 🧩 Enterprise Chain Builder (Important)

Instead of manual linking in controllers — enterprise uses Pipeline Builder Service.

    public class OrderPipeline
    {
        private readonly IEnumerable<IOrderHandler> _handlers;

        public OrderPipeline(IEnumerable<IOrderHandler> handlers)
        {
            _handlers = handlers;
        }

        public IOrderHandler Build()
        {
            IOrderHandler first = null;
            IOrderHandler current = null;

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

####  ⚙️ Dependency Injection (Enterprise Level)
    builder.Services.AddScoped<IOrderHandler, ValidationHandler>();
    builder.Services.AddScoped<IOrderHandler, FraudCheckHandler>();
    builder.Services.AddScoped<IOrderHandler, PaymentHandler>();
    builder.Services.AddScoped<IOrderHandler, OrderCreationHandler>();

    builder.Services.AddScoped<OrderPipeline>();


THIS is real enterprise pattern usage.

####  🎮 Controller Usage / Or any other class where you want to use the COR

    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderPipeline _pipeline;

        public OrderController(OrderPipeline pipeline)
        {
            _pipeline = pipeline;
        }

        [HttpPost("/order")]
        public async Task<IActionResult> CreateOrder()
        {
            var context = new OrderContext
            {
                UserId = "Yash",
                Amount = 5000
            };

            var chain = _pipeline.Build();

            await chain.HandleAsync(context);

            return Ok();
        }
    }

### 🌍 Where THIS Exact Enterprise Pattern Exists

You have already used similar architecture in:

ASP.NET Core
- middleware pipeline

MediatR
- pipeline behaviors

MassTransit / NServiceBus
- message pipelines

Payment Gateways
- rule engines
- fraud checks
- retries
- logging chains

Security Systems
- auth
- policy
- auditing

### Enterprisen Angle:
#### In enterprise systems Chain of Responsibility is commonly implemented as processing pipelines using DI-driven handler registration, allowing dynamic execution flow without modifying orchestration logic

⚠️ Very Important Enterprise Nuance

Enterprise CoR often becomes:
- Pipeline Pattern
- Middleware Pattern
- Behavior Pipeline
- Processing Pipeline
Same core idea.


#### References:

- [Refactoring Guru - COR](https://refactoring.guru/design-patterns/chain-of-responsibility)
- [Dot Net Tutorials - COR](https://dotnettutorials.net/lesson/chain-of-responsibility-design-pattern/#google_vignette)
- [Medium - COR](https://mohamed-hendawy.medium.com/chain-of-responsibility-design-pattern-in-c-with-examples-d87da6e5ead)