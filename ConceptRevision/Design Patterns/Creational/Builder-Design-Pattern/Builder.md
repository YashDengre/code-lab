### 🧱 Builder Design Pattern
✅ Definition

Builder Pattern is a creational design pattern used to construct complex objects step-by-step while allowing the same construction process to create different representations.

It separates:👉 Object construction logic from 👉 Object representation

#### 🎯 Why This Pattern?

When:

- Object has many optional fields
- Object construction requires multiple steps
- You want readable object creation
- Constructor becomes ugly:

		 new User("Yash", "Dengre", true, true, false, null, "Admin", DateTime.Now);
- Hard to read
- Hard to maintain
- Error-prone


#### 🚨 Problem It Solves
❌ Telescoping Constructor Problem

Too many constructors:

	User(string name)
	User(string name, string role)
	User(string name, string role, bool isAdmin)
	User(string name, string role, bool isAdmin, bool isActive)

Explosion of constructors 😵‍💫

❌ Object Creation Logic Mixed With Business Logic

Developers start doing:

	var user = new User();
	user.Name = "Yash";
	user.Role = "Admin";
	user.IsActive = true;


- No validation
- No control
- Incomplete objects possible

### ⚙️ How Builder Solves It

Builder:

- Provides step-by-step creation
- Hides internal construction
- Ensures valid object
- Makes object creation readable
- Supports immutabilit

Flow:

	Client → Builder → Product


### 🧪 Simple Example: (Conceptual Understanding)

Scenario

#### Build a Computer

Step 1 — Product

    public class Computer
    {
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string Storage { get; set; }

        public override string ToString()
        {
            return $"CPU: {CPU}, RAM: {RAM}, Storage: {Storage}";
        }
    }

Step 2 — Builder Interface

    public interface IComputerBuilder
    {
        void SetCPU(string cpu);
        void SetRAM(string ram);
        void SetStorage(string storage);
        Computer Build();
    }

Step 3 — Concrete Builder

    public class ComputerBuilder : IComputerBuilder
    {
        private Computer _computer = new Computer();

        public void SetCPU(string cpu)
        {
            _computer.CPU = cpu;
        }

        public void SetRAM(string ram)
        {
            _computer.RAM = ram;
        }

        public void SetStorage(string storage)
        {
            _computer.Storage = storage;
        }

        public Computer Build()
        {
            return _computer;
        }
    }

Step 4 — Director (Optional but Classic)

    public class ComputerDirector
    {
        public Computer BuildGamingPC(IComputerBuilder builder)
        {
            builder.SetCPU("i9");
            builder.SetRAM("32GB");
            builder.SetStorage("2TB SSD");
            return builder.Build();
        }
    }

Step 5 — Usage

    var builder = new ComputerBuilder();
    var director = new ComputerDirector();

    var gamingPc = director.BuildGamingPC(builder);

    Console.WriteLine(gamingPc);


### 🏢 Enterprise Real-Time Example (VERY IMPORTANT FOR INTERVIEWS)

Scenario: Building API Request / Domain Aggregate

Example:
👉 Order Creation in Enterprise System

Object may contain:

- Customer
- Items
- Pricing
- Discount
- Tax
- Audit Info
- Metadata
- Validation

We want:

- Immutable object
- Validation before build
- Fluent readable construction

Step 1 — Product (Domain Entity)

    public class Order
    {
        public string CustomerId { get; }
        public List<string> Items { get; }
        public decimal Discount { get; }
        public decimal Tax { get; }
        public DateTime CreatedAt { get; }

        public Order(
            string customerId,
            List<string> items,
            decimal discount,
            decimal tax,
            DateTime createdAt)
        {
            CustomerId = customerId;
            Items = items;
            Discount = discount;
            Tax = tax;
            CreatedAt = createdAt;
        }
    }

Step 2 — Enterprise Builder (Fluent)

    public class OrderBuilder
    {
        private string _customerId;
        private List<string> _items = new();
        private decimal _discount;
        private decimal _tax;

        public OrderBuilder ForCustomer(string customerId)
        {
            _customerId = customerId;
            return this;
        }

        public OrderBuilder AddItem(string item)
        {
            _items.Add(item);
            return this;
        }

        public OrderBuilder ApplyDiscount(decimal discount)
        {
            _discount = discount;
            return this;
        }

        public OrderBuilder ApplyTax(decimal tax)
        {
            _tax = tax;
            return this;
        }

        public Order Build()
        {
            if (string.IsNullOrEmpty(_customerId))
                throw new Exception("Customer required");

            if (!_items.Any())
                throw new Exception("Order must have items");

            return new Order(
                _customerId,
                _items,
                _discount,
                _tax,
                DateTime.UtcNow);
        }
    }

Step 3 — Usage (Enterprise Service Layer)

    var order = new OrderBuilder()
        .ForCustomer("CUST-1001")
        .AddItem("Laptop")
        .AddItem("Mouse")
        .ApplyDiscount(10)
        .ApplyTax(5)
        .Build();

🏢 Real Enterprise Usage You Already Saw (Even If You Didn't Notice)

ASP.NET Core

    var host = Host.CreateDefaultBuilder()


👉 Builder pattern

HttpClient

    new HttpRequestMessageBuilder()

Entity Framework Core

    modelBuilder.Entity<User>()

Azure SDK

    BlobClientOptionsBuilder()

Microservices Domain Modeling

- Aggregate root creation
- Event creation
- Complex DTO assembly
- Pipeline configuration


### Builder is ideal when object construction is complex, involves multiple optional steps, and must enforce validation while keeping the product immutable and readable

### 🧱 Builder Pattern — Enterprise Level Example
📌 Enterprise Scenario

👉 Problem Statement (Real World)

🧱 Builder Pattern — Enterprise Level Example

You are building an Enterprise Order Processing System

An order creation involves:

- Customer info
- Multiple order items
- Pricing calculations
- Discounts & coupons
- Tax calculation
- Shipping strategy
- Payment configuration
- Audit metadata
- Optional features (gift wrap, insurance, priority delivery)

⚠️ Problems without Builder:

- Massive constructor with 15+ params
- Optional configurations messy
- Different order types (Standard / Corporate / International)
- Validation & construction mixed everywhere
- Hard to extend

🎯 Why Builder Here?

Because:

- Object creation is complex
- Steps are optional & conditional
- Construction must be controlled
- Same process but different configurations

🧠 How Builder Solves

Builder:

- Separates construction from representation
- Provides step-by-step creation
- Supports optional components
- Keeps object immutable & valid
- Allows reusable build pipelines

### 🏗️ Enterprise Implementation (Complete Flow)

We will build:

    Order
    OrderBuilder
    Concrete Builders
    Director (OrderProcessingService)
    Usage Example

#### ✅ Step 1 — Domain Model (Enterprise Order)

    public class Order
    {
        public Guid Id { get; private set; }
        public string CustomerId { get; private set; }
        public List<string> Items { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal Tax { get; private set; }
        public decimal Discount { get; private set; }
        public decimal ShippingCost { get; private set; }
        public decimal Total { get; private set; }
        public string PaymentMethod { get; private set; }
        public DateTime CreatedOn { get; private set; }

        internal Order(
            Guid id,
            string customerId,
            List<string> items,
            decimal subTotal,
            decimal tax,
            decimal discount,
            decimal shippingCost,
            decimal total,
            string paymentMethod,
            DateTime createdOn)
        {
            Id = id;
            CustomerId = customerId;
            Items = items;
            SubTotal = subTotal;
            Tax = tax;
            Discount = discount;
            ShippingCost = shippingCost;
            Total = total;
            PaymentMethod = paymentMethod;
            CreatedOn = createdOn;
        }
    }

👉 Notice:

Constructor is internal

Object created only via Builder

#### ✅ Step 2 — Builder Interface

    public interface IOrderBuilderV2
        {
            void SetCustomer(string customerId);
            void AddItems(List<string> items);
            void CalculatePricing();
            void ApplyDiscount();
            void CalculateTax();
            void ConfigureShipping();
            void ConfigurePayment(string method);
            OrderV2 Build();
        }


#### ✅ Step 3 — Concrete Enterprise Builder

    public class StandardOrderBuilder : IOrderBuilder
    {
        private string _customerId;
        private List<string> _items = new();
        private decimal _subTotal;
        private decimal _tax;
        private decimal _discount;
        private decimal _shipping;
        private string _paymentMethod;

        public void SetCustomer(string customerId)
        {
            _customerId = customerId;
        }

        public void AddItems(List<string> items)
        {
            _items.AddRange(items);
        }

        public void CalculatePricing()
        {
            _subTotal = _items.Count * 100; // simulate pricing service
        }

        public void ApplyDiscount()
        {
            _discount = _subTotal > 500 ? 50 : 0;
        }

        public void CalculateTax()
        {
            _tax = (_subTotal - _discount) * 0.18m;
        }

        public void ConfigureShipping()
        {
            _shipping = 40;
        }

        public void ConfigurePayment(string method)
        {
            _paymentMethod = method;
        }

        public Order Build()
        {
            var total = _subTotal - _discount + _tax + _shipping;

            return new Order(
                Guid.NewGuid(),
                _customerId,
                _items,
                _subTotal,
                _tax,
                _discount,
                _shipping,
                total,
                _paymentMethod,
                DateTime.UtcNow);
        }
    }

#### ✅ Step 4 — Director (Enterprise Orchestration Layer)

👉 In enterprise systems this is usually:

- Application Service
- Domain Service
- Workflow Handler
- Orchestration Layer

        public class OrderProcessingService
        {
            public Order CreateStandardOrder(
                IOrderBuilder builder,
                string customerId,
                List<string> items,
                string paymentMethod)
            {
                builder.SetCustomer(customerId);
                builder.AddItems(items);
                builder.CalculatePricing();
                builder.ApplyDiscount();
                builder.CalculateTax();
                builder.ConfigureShipping();
                builder.ConfigurePayment(paymentMethod);

                return builder.Build();
            }
        }

#### Step 5 — Enterprise Usage

    var builder = new StandardOrderBuilder();
    var service = new OrderProcessingService();

    var order = service.CreateStandardOrder(
        builder,
        "CUST-1001",
        new List<string> { "Laptop", "Mouse", "Keyboard" },
        "CreditCard");

    Console.WriteLine($"Order Total: {order.Total}");


#### 🏢 REAL ENTERPRISE USE CASES

You have already seen Builder in:

1️⃣ ASP.NET Core Host Builder

    var builder = WebApplication.CreateBuilder(args)
        .Services
        .AddControllers()
        .AddAuthorization();


Massive enterprise builder.

2️⃣ HttpClient Builder

    services.AddHttpClient("api")
            .AddPolicyHandler(retryPolicy)
            .AddHttpMessageHandler<AuthHandler>();

3️⃣ EF Core Model Builder

    modelBuilder.Entity<User>()
        .HasKey(x => x.Id)
        .HasIndex(x => x.Email);

4️⃣ Azure SDK Clients

    var client = new BlobClientBuilder()
        .WithConnectionString(conn)
        .WithRetryPolicy(policy)
        .Build();

🔥 Interview One-Liner (Senior Level)

Builder is used when object construction involves multiple optional steps, complex configuration, or different representations. It separates the construction process from the final object and enables controlled, readable, and extensible creation flows — commonly seen in ASP.NET Core configuration, EF Core model building, and enterprise workflow orchestration

References:
- [Refactoring Guru - Builder](https://refactoring.guru/design-patterns/builder)
- [DOT NET Tutorials - Builder](https://dotnettutorials.net/lesson/builder-design-pattern/)