### Mediator Pattern

### ✅ 1. What is Mediator Pattern

Mediator is a behavioral pattern where:

- 👉 Objects do not communicate directly
- 👉 Instead they communicate through a central mediator

So instead of:

	Controller → Service A → Service B → Service C


You get:

	Controller → Mediator → Handler

### ✅ 2. Why This Pattern Exists
The real enterprise problem:

When systems grow:

- Services start calling each other
- Dependencies explode
- Testing becomes painful
- Code becomes tightly coupled

Example without mediator:

	OrderService -> PaymentService
	OrderService -> EmailService
	OrderService -> InventoryService
	OrderService -> AuditService


Now OrderService knows too much.

### ✅ 3. What Problem Mediator Solves

|Problem	|Mediator Solution|
|-----------|-----------------|
|Tight coupling|	Central communication|
|Hard testing	|Handlers isolated|
|Spaghetti service calls|	Request/Handler model|
|Huge services	|Single responsibility handlers|
|Complex orchestration	|Mediator routing|

### ✅ 4. Enterprise Reality (.NET)

Most enterprise .NET apps use:

	👉 MediatR library

But today we build our own enterprise-style mediator
so you actually understand internals.


### 🏗️ 5. Enterprise Example (Customer CQRS Style)

We will build:

- Request (Command / Query)
- Handler
- Mediator
- Dispatcher
- Controller usage

Step 1 — Request Interfaces

    public interface IRequest<TResponse> {}

    public interface IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }

Step 2 — Mediator Interface

    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
    }

Step 3 — Enterprise Mediator Implementation

    public class Mediator : IMediator
    {
        private readonly IServiceProvider _provider;

        public Mediator(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var handlerType = typeof(IRequestHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            dynamic handler = _provider.GetService(handlerType);

            return await handler.Handle((dynamic)request);
        }
    }


👉 This is exactly how enterprise mediator works internally.

Step 4 — Customer Command

    public class CreateCustomerCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

Step 5 — Command Handler

    public class CreateCustomerHandler
        : IRequestHandler<CreateCustomerCommand, int>
    {
        public async Task<int> Handle(CreateCustomerCommand request)
        {
            Console.WriteLine($"Customer Created: {request.Name}");
            return 1;
        }
    }

Step 6 — Query

    public class GetCustomerQuery : IRequest<string>
    {
        public int Id { get; set; }
    }

Step 7 — Query Handler

    public class GetCustomerHandler
        : IRequestHandler<GetCustomerQuery, string>
    {
        public async Task<string> Handle(GetCustomerQuery request)
        {
            return $"Customer-{request.Id}";
        }
    }

Step 8 — DI Registration

    services.AddScoped<IMediator, Mediator>();

    services.AddScoped<
        IRequestHandler<CreateCustomerCommand, int>,
        CreateCustomerHandler>();

    services.AddScoped<
        IRequestHandler<GetCustomerQuery, string>,
        GetCustomerHandler>();

Step 9 — Controller (Enterprise Usage)

    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var result = await _mediator.Send(
                new CreateCustomerCommand { Name = "Yash" });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(
                new GetCustomerQuery { Id = id });

            return Ok(result);
        }
    }

### 🏢 Real Enterprise Usage

Mediator is used for:

- CQRS commands & queries
- Domain events
- Workflow orchestration
- Validation pipelines
- Logging pipelines
- Authorization pipelines
- Background job execution
- Microservice communication handlers

⚡ Advanced Enterprise Feature (VERY IMPORTANT)

Mediator enables:

👉 Pipeline Behaviors

Like:

- Validation
- Logging
- Transaction handling
- Retry logic
- Performance monitoring

Before handler executes.

Huge enterprise advantage.

### 🎯 Simple Mental Model

Without Mediator:

    Controller → Service → Service → Repo


With Mediator:

    Controller → Mediator → Handler


Handlers are:

- small
- isolated
- testable
- replaceable

### 🧠 One Line Interview Answer

Mediator centralizes communication between components by routing requests through handlers, reducing direct dependencies and enabling scalable CQRS-style architectures with pipeline extensibility.


### Queries:
- Why some examples are having one mediator which talks to source to different destinations.
- Why some examples are having one mediator within each component and they talk to each other through that only.


Good observation 👍 — you’ve actually spotted the two different styles people call “Mediator,” and yes, they can look quite different.

#### 🔹 Classic GoF Mediator (UI / component communication style)

- Each component has a reference to a mediator
- Components talk to mediator, mediator routes messages to others 
- Example:

    Button → Mediator → DialogBox → TextBox


- Used in:

    - UI frameworks
    - Chat systems
    - Workflow engines
    - Complex object interaction scenarios

👉 Goal = decouple many objects from each other

#### 🔹 Enterprise .NET Mediator (MediatR / CQRS style — what we built)

- Single mediator
- Requests (Command/Query/Event) go through mediator
- Mediator resolves handlers
- No direct component-to-component messaging

👉 Goal = decouple application flow + orchestration

Architecture becomes:

    Controller → Mediator → Handler

#### 🔹 Why ours looked like Facade to you

Nice catch — because:

- Controller sends one request
- Mediator routes internally
- Handler executes business logic

So visually it feels like:

    Controller → Facade → Services  

But internally:

- mediator = dispatcher/router
- handlers = independent execution units

So intent is different even if flow looks similar.

#### 🎯 Ultra short differentiation

|Style	| Usage|
|-------|-------|
|Classic Mediator	|Object-to-object communication|
|Enterprise Mediator|	Request-to-handler dispatch|

### 🎯 Classic Mediator Pattern — Enterprise Example

#### ✅ What Mediator Actually Is (real definition)

Mediator centralizes communication & coordination between multiple components so they:

- don’t directly depend on each other
- don’t create circular dependencies
- don’t know who else exists

    Component → Mediator → Other Components

### 🧠 Real Enterprise Use Case

#### 🏢 Complex Order Processing Workflow

Multiple subsystems:

- Payment Service
- Inventory Service
- Notification Service
- Fraud Check Service
- Shipping Service

Without mediator:

    Payment calls Inventory
    Inventory calls Notification
    Fraud calls Shipping
    Shipping calls Payment


👉 becomes a dependency nightmare.

With Mediator:

    All communicate through OrderMediator

### 🏗️ Structure

    IOrderMediator
        ↑
    OrderMediator

    IComponent
        ↑
    PaymentComponent
    InventoryComponent
    ShippingComponent
    NotificationComponent
    FraudComponent

#### ✅ Step 1 — Mediator Contract

    public interface IOrderMediator
    {
        Task NotifyAsync(string eventType, object data);
    }

#### ✅ Step 2 — Base Component

    public abstract class OrderComponent
    {
        protected IOrderMediator _mediator;

        protected OrderComponent(IOrderMediator mediator)
        {
            _mediator = mediator;
        }
    }

#### ✅ Step 3 — Concrete Components
💳 Payment

    public class PaymentComponent : OrderComponent
    {
        public PaymentComponent(IOrderMediator mediator) : base(mediator) {}

        public async Task ProcessPaymentAsync(int orderId)
        {
            Console.WriteLine("Payment processed");

            await _mediator.NotifyAsync("PaymentCompleted", orderId);
        }
    }

📦 Inventory

    public class InventoryComponent : OrderComponent
    {
        public InventoryComponent(IOrderMediator mediator) : base(mediator) {}

        public async Task ReserveAsync(int orderId)
        {
            Console.WriteLine("Inventory reserved");

            await _mediator.NotifyAsync("InventoryReserved", orderId);
        }
    }

🚚 Shipping

    public class ShippingComponent : OrderComponent
    {
        public ShippingComponent(IOrderMediator mediator) : base(mediator) {}

        public Task CreateShipmentAsync(int orderId)
        {
            Console.WriteLine("Shipment created");
            return Task.CompletedTask;
        }
    }

📢 Notification

    public class NotificationComponent : OrderComponent
    {
        public NotificationComponent(IOrderMediator mediator) : base(mediator) {}

        public Task SendAsync(string message)
        {
            Console.WriteLine($"Notification: {message}");
            return Task.CompletedTask;
        }
    }

🔍 Fraud Check

    public class FraudComponent : OrderComponent
    {
        public FraudComponent(IOrderMediator mediator) : base(mediator) {}

        public async Task ValidateAsync(int orderId)
        {
            Console.WriteLine("Fraud check completed");

            await _mediator.NotifyAsync("FraudValidated", orderId);
        }
    }

✅ Step 4 — Enterprise Mediator Implementation

    public class OrderMediator : IOrderMediator
    {
        public PaymentComponent Payment { get; set; }
        public InventoryComponent Inventory { get; set; }
        public ShippingComponent Shipping { get; set; }
        public NotificationComponent Notification { get; set; }
        public FraudComponent Fraud { get; set; }

        public async Task NotifyAsync(string eventType, object data)
        {
            var orderId = (int)data;

            switch (eventType)
            {
                case "PaymentCompleted":
                    await Fraud.ValidateAsync(orderId);
                    break;

                case "FraudValidated":
                    await Inventory.ReserveAsync(orderId);
                    break;

                case "InventoryReserved":
                    await Shipping.CreateShipmentAsync(orderId);
                    await Notification.SendAsync("Order shipped");
                    break;
            }
        }
    }

✅ Step 5 — Wiring (Enterprise Composition Root)

    var mediator = new OrderMediator();

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

### 🧠 Why This Is Real Enterprise Mediator
Without mediator

    Payment → Fraud
    Fraud → Inventory
    Inventory → Shipping
    Shipping → Notification


Every class depends on multiple others 😬

With mediator

Each component knows only:

    IOrderMediator


No direct coupling.

### 🎯 Enterprise Benefits

- #### ✔️ Removes circular dependencies
- #### ✔️ Centralized workflow orchestration
- #### ✔️ Allows dynamic workflow change
- #### ✔️ Great for workflow engines / process managers
- #### ✔️ Improves testability
- #### ✔️ Easier onboarding for large teams

### ⚠️ Real Industry Usage Areas

- Order Processing Engines
- UI Dialog Controllers (WPF / Blazor complex screens)
- Workflow Engines
- Saga Coordinators
- Game Engines
- Financial Transaction Pipelines
- Distributed Business Processes

### 📌 One Real Senior Insight (important)

In enterprise systems:

👉 Mediator often evolves into:

- Process Manager
- Saga Orchestrator
- Workflow Coordinator


### Enterprise Mediator Pipeline Behavior Exmaple:

Perfect 👍 now we move to the real enterprise Mediator usage —
not the classic object-talking mediator… but the one you actually see in modern .NET systems:

#### 👉 Mediator Pipeline Behavior (MediatR style / CQRS style)

This is extremely important for senior interviews.

### 🎯 What is Mediator Pipeline Behavior (Enterprise Reality)

Classic Mediator:

    Components talk through mediator


Enterprise Mediator:

    Request → Mediator → Pipeline Behaviors → Handler


Mediator becomes:

- 👉 Request Dispatcher
- 👉 Cross-cutting orchestrator
- 👉 Execution pipeline


### 🧠 Real Enterprise Problems It Solves

In large systems every request needs:

- Validation
- Logging
- Authorization
- Transactions
- Performance monitoring
- Retry
- Exception handling

Without pipeline:

    Every handler duplicates logic 😭


Pipeline fixes:

    Behavior → Behavior → Behavior → Handler


Same concept as ASP.NET middleware but inside application layer.


### 🏢 Enterprise Example — Customer CQRS

We will build:

    CreateCustomerCommand
    CreateCustomerHandler
    ValidationBehavior
    LoggingBehavior
    TransactionBehavior
    Mediator


#### ✅ Step 1 — Request Contract

    public interface IRequest<TResponse> { }

    public interface IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }

#### ✅ Step 2 — Pipeline Behavior Contract

    public interface IPipelineBehavior<TRequest, TResponse>
    {
        Task<TResponse> Handle(
            TRequest request,
            Func<Task<TResponse>> next);
    }

#### ✅ Step 3 — Command

    public class CreateCustomerCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

#### ✅ Step 4 — Handler

    public class CreateCustomerHandler 
        : IRequestHandler<CreateCustomerCommand, int>
    {
        public Task<int> Handle(CreateCustomerCommand request)
        {
            Console.WriteLine($"Customer Created: {request.Name}");
            return Task.FromResult(1001);
        }
    }

#### ✅ Step 5 — Enterprise Behaviors
🔍 Validation

    public class ValidationBehavior<TRequest,TResponse>
        : IPipelineBehavior<TRequest,TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            Func<Task<TResponse>> next)
        {
            Console.WriteLine("Validation executed");
            return await next();
        }
    }

📊 Logging

    public class LoggingBehavior<TRequest,TResponse>
        : IPipelineBehavior<TRequest,TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            Func<Task<TResponse>> next)
        {
            Console.WriteLine("Logging before");
            var result = await next();
            Console.WriteLine("Logging after");
            return result;
        }
    }

💾 Transaction

    public class TransactionBehavior<TRequest,TResponse>
        : IPipelineBehavior<TRequest,TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            Func<Task<TResponse>> next)
        {
            Console.WriteLine("Transaction Started");

            var result = await next();

            Console.WriteLine("Transaction Committed");
            return result;
        }
    }

✅ Step 6 — Enterprise Mediator

    public class Mediator
    {
          var requestType = request.GetType();
          var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
          var handler = _serviceProvider.GetRequiredService(handlerType);

          var behaviorType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, typeof(TResponse));
          var behaviors = _serviceProvider.GetServices(behaviorType).Cast<dynamic>();

          Func<Task<TResponse>> handlerDelegate = () => ((dynamic)handler).Handle((dynamic)request); // either use dynamic type in handler or get the method by handlerType.GetMethod("Handle")

          foreach (var behavior in behaviors.Reverse())
          {
              var next = handlerDelegate;
              handlerDelegate = () => behavior.Handle((dynamic)request, next);
          }
          return await handlerDelegate();
    }


(Conceptual — real MediatR handles this internally)

#### ✅ Step 7 — Execution Flow
Mediator.Send()

    → ValidationBehavior
    → LoggingBehavior
    → TransactionBehavior
    → Handler


Console Output:

    Validation executed
    Logging before
    Transaction Started
    Customer Created
    Transaction Committed
    Logging after

### 🧠 THIS is why Enterprise Uses Mediator Pipelines

- #### 🔥 Removes cross-cutting duplication
- #### 🔥 Enforces clean architecture
- #### 🔥 Adds plug-and-play behaviors
- #### 🔥 Centralizes request execution
- #### 🔥 Enables audit & governance layers
- #### ⚠️ VERY IMPORTANT Enterprise Reality

Most modern .NET enterprise systems use:

- 👉 MediatR library
- 👉 CQRS + Pipeline Behavior
- 👉 Not classic component mediator