### Facade Pattern:
Facade is a structural design pattern that provides a simplified interface to a library, a framework, or any other complex set of classes.
Facade Design Pattern states that you need to provide a unified interface to a set of interfaces in a subsystem. The Facade Design Pattern defines a higher-level interface that makes the subsystem easier to use.

Facade = a single simplified interface that hides a complex system behind it.

👉 Instead of the client talking to 10 different classes, it talks to one class (Facade), and the facade internally coordinates everything.

Think: “Don’t expose complexity to the caller — give them one clean entry point.”

(Ref: https://dotnettutorials.net/lesson/facade-design-pattern/) As the name suggests, Facade means the Face of the Building. Suppose you created one building. The people walking outside the building can only see the walls and glass of the Building. The People do not know anything about the wiring, the pipes, the interiors, and other complexities inside the building. That means the Facade hides all the complexities of the building and displays a friendly face to people walking outside the building.

Reference: https://refactoring.guru/design-patterns/facade


🎬 Real-Life Analogy (Interview Gold)
🍿 Home Theater System

Without Facade:
- Turn on projector
- Turn on sound system
- Configure input
- Dim lights
- Start media player
 
With Facade: homeTheaterFacade.WatchMovie() => One method → multiple operations internally.


	🏗️ Structure
	- Components:
		- Subsystem classes
			- Complex logic
			- Multiple dependencies
	- Facade
		- Knows how to call subsystem classes
		- Provides simple methods
	- Client
		- Calls facade instead of subsystems

💻 .NET Practical Example (Realistic Enterprise Style)

❌ Without Facade

Imagine Order Placement:

	paymentService.ProcessPayment();
	inventoryService.ReserveItems();
	shippingService.CreateShipment();
	notificationService.SendEmail();

	Client knows everything → tight coupling 😬

✅ With Facade
Subsystems

	public class PaymentService
	{
		public void ProcessPayment() { }
	}

	public class InventoryService
	{
		public void ReserveItems() { }
	}

	public class ShippingService
	{
		public void CreateShipment() { }
	}

Facade

	public class OrderFacade
	{
		private readonly PaymentService _payment;
		private readonly InventoryService _inventory;
		private readonly ShippingService _shipping;

		public OrderFacade(
			PaymentService payment,
			InventoryService inventory,
			ShippingService shipping)
		{
			_payment = payment;
			_inventory = inventory;
			_shipping = shipping;
		}

		public void PlaceOrder()
		{
			_payment.ProcessPayment();
			_inventory.ReserveItems();
			_shipping.CreateShipment();
		}
	}
Client

	orderFacade.PlaceOrder();
	Client = clean
	System = still modular


🎯 When to Use Facade (Interview Answer)

Say this:

- ✅ Complex subsystem
- ✅ Many dependencies
- ✅ You want to reduce coupling
- ✅ You want a cleaner API for clients
- ✅ Legacy system wrapper
- ✅ Layered architecture entry point


❌ When NOT to Use

- 🚫 When system is already simple
- 🚫 When facade becomes a GOD class
- 🚫 When you need dynamic behavior changes (Strategy better)

⚡ Facade vs Mediator (Interview Trap)

| Facade |	Mediator|
|--------|----------|
| Simplifies external usage |	Manages communication between objects |
| Client facing |	Internal coordination |
| One-way simplification |	Two-way interaction control |


🧱 Real Enterprise Examples (Say these in Interview)

- Payment Gateway Wrapper
- External API SDK wrapper
- Microservice Aggregator Layer
- BFF (Backend for Frontend)
- Complex Azure Service orchestration wrapper
- Repository + UnitOfWork combined facade


### Code Exmaple:

#### 🛒 Scenario: Order Processing in E-Commerce System

Imagine a real production system when a user places an order.

Behind the scenes multiple subsystems exist:

- Inventory Service
- Payment Service
- Fraud Check Service
- Shipping Service
- Notification Service
- Logging / Auditing
- Discount Engine

👉 Without facade → Controllers/Consumers become monsters.


#### ❌ Without Facade (Real Enterprise Problem)

Your API Controller starts doing this:

	Validate Order
	Check Inventory
	Apply Discounts
	Run Fraud Detection
	Process Payment
	Reserve Stock
	Create Shipment
	Send Email
	Write Audit Logs


Controller becomes:

- tightly coupled
- impossible to test
- messy orchestration logic
- violates SRP


#### ✅ With Facade — Clean Enterprise Structure

We introduce:
	
	OrderProcessingFacade

Controller talks to ONE class.

Facade orchestrates complex workflow internally.

🏗️ Enterprise Architecture View

	API Controller
     |
     v
	OrderProcessingFacade
		 |
	--------------------------------------
	| InventoryService                  |
	| PaymentService                    |
	| FraudService                      |
	| ShippingService                   |
	| NotificationService               |
	--------------------------------------


- 👉 Controller = simple
- 👉 Facade = workflow orchestrator
- 👉 Services = business units

Refer code: [Path]('Facade.cs')


🎯 Enterprise-Level Interview Talking Points (VERY IMPORTANT)

When you explain Facade in interviews — say this:

Facade is used for:

- ✅ Simplifying complex workflows
- ✅ Orchestrating multiple services
- ✅ Protecting controllers from business complexity
- ✅ Providing a unified entry point
- ✅ Reducing coupling between layers

⚠️ Real Industry Usage (You have probably already used it 😄)

In enterprise .NET projects facade appears as:

|Name Used in Industry|	Actually a Facade|
|---------------------|------------------|
|Application Service  |	    ✅ Yes		|
|Orchestration Service|	✅ Yes|
|Workflow Manager|	✅ Yes|
|UseCase Handler|	✅ Yes|
|Manager Class	|Often yes|
|Service Layer Aggregator |Yes|

Most devs use Facade without realizing it.

🧠 Interview GOLD Question

Q: How is Facade different from Service Layer?

- Service Layer = business rules
- Facade = workflow simplification + orchestration across services

🚨 Advanced Insight (Senior Level)

Facade SHOULD:

- not contain heavy business logic
- not become God Class
- only coordinate services
- remain thin orchestration layer