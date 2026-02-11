### 🧬 Prototype Pattern
#### 📘 Definition: 
#### Prototype pattern creates new objects by cloning an existing instance instead of constructing from scratch.

👉 Object creation is based on copying an existing “prototype”.

#### ❓ Why this pattern exists

Traditional object creation can be:

- expensive (DB calls, config loading, heavy initialization)
- complex (nested objects / aggregates)
- dynamic (runtime template objects)
 
Instead of:

	new ComplexObject(a,b,c,...)

we do:

	prototype.Clone()

#### 🚨 Problem it solves
1️⃣ Expensive object creation

- ML models
- document templates
- configuration graphs
- large aggregates
 
2️⃣ Deep object graphs

- nested collections
- hierarchical structures

3️⃣ Runtime templating

- cloning request templates
= cloning workflow definitions

4️⃣ Avoid constructor explosion

- Instead of 15 parameters → clone + modify few fields

#### ⚙️ How it solves

- Maintain a base object (prototype)
- Provide cloning capability
- Create new instances by copying
- Modify only needed properties

#### 🔥 Enterprise Level Example
Scenario: Customer Aggregate Template System

- Enterprise SaaS system where:

- default customer setup exists

- includes:
	- address
	- permissions
	- preferences
	- tags

- new customers start from template


#### Step 1 — Prototype Contract

    public interface IPrototype<T>
    {
        T Clone();
    }

#### Step 2 — Domain Models

    public class Address
    {
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class CustomerSettings
    {
        public bool EmailNotifications { get; set; }
        public List<string> Tags { get; set; } = new();
    }

#### Step 3 — Customer Aggregate (Prototype)

    public class Customer : IPrototype<Customer>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public CustomerSettings Settings { get; set; }

        public Customer Clone()
        {
            return new Customer
            {
                Id = Guid.NewGuid(),
                Name = this.Name,
                Address = new Address
                {
                    City = this.Address.City,
                    Country = this.Address.Country
                },
                Settings = new CustomerSettings
                {
                    EmailNotifications = this.Settings.EmailNotifications,
                    Tags = new List<string>(this.Settings.Tags)
                }
            };
        }
    }


⚠️ This is deep cloning — enterprise critical.

#### Step 4 — Prototype Registry (Very Enterprise)

    public interface ICustomerPrototypeRegistry
    {
        Customer GetTemplate(string key);
    }

    public class CustomerPrototypeRegistry : ICustomerPrototypeRegistry
    {
        private readonly Dictionary<string, Customer> _templates;

        public CustomerPrototypeRegistry()
        {
            _templates = new Dictionary<string, Customer>
            {
                ["default"] = new Customer
                {
                    Name = "Template",
                    Address = new Address
                    {
                        City = "Pune",
                        Country = "India"
                    },
                    Settings = new CustomerSettings
                    {
                        EmailNotifications = true,
                        Tags = new List<string> { "NewCustomer" }
                    }
                }
            };
        }

        public Customer GetTemplate(string key)
        {
            return _templates[key].Clone();
        }
    }

#### Step 5 — Application Service

    public class CustomerService
    {
        private readonly ICustomerPrototypeRegistry _registry;

        public CustomerService(ICustomerPrototypeRegistry registry)
        {
            _registry = registry;
        }

        public Customer CreateCustomerFromTemplate(string templateKey, string name)
        {
            var customer = _registry.GetTemplate(templateKey);

            customer.Name = name;
            customer.Id = Guid.NewGuid();

            return customer;
        }
    }

### 🏢 Real Enterprise Usage

1️⃣ Document Templates

- invoices
- reports
- contracts

2️⃣ Workflow Engines

- cloning job pipelines
- approval flows

3️⃣ Game Engines / Simulations

- NPC templates
- environment setups

4️⃣ SaaS Multi-tenant Setup

- default tenant config
- cloned environments

5️⃣ Infrastructure-as-Code Systems

- resource templates
- deployment configs

### Enterprise Pitfalls (Important)

❌ Shallow copy

    MemberwiseClone()

leads to shared references → production bugs.

❌ Mutable prototypes
 
 - Templates should be immutable or protected.

❌ Forgetting nested collections

- Always deep copy lists and objects.

### 🎯 When NOT to use Prototype

- simple objects
- stateless services
- EF tracked entities (dangerous!)
- DTO mapping scenarios (use AutoMapper)

### 🧠 Interview Summary (Enterprise Answer)

#### Prototype is used when object creation is expensive or complex. Instead of constructing new instances from scratch, we clone existing configured objects. In enterprise systems it’s used for templates, workflows, configuration graphs, and aggregate initialization. The key concern is deep cloning to avoid shared mutable state.


### Questions:

#### ✅ 1. Are Builder & Prototype mostly used for model/object creation?

👉 YES — your understanding is correct.

Mostly used when:

- complex domain models / aggregates
- configuration objects
- templates
- request/response pipelines
- workflow definitions
- infrastructure configs

Not limited to “models”, but object construction problems are the primary use case.

So your statement is technically correct ✔️

#### ✅ 2. Missing Final Usage Example (Clone from registry & direct object)
👉 Using Prototype Registry (Enterprise Style)

    var registry = new CustomerPrototypeRegistry();

    var templateCustomer = registry.GetTemplate("default");

    templateCustomer.Name = "Yash Dengre";

    Console.WriteLine(templateCustomer.Name);


👉 Internally:

- registry returns Clone()
- you modify copy safely

👉 Direct Clone From Existing Object

    var original = new Customer
    {
        Name = "Base Customer",
        Address = new Address { City = "Pune", Country = "India" },
        Settings = new CustomerSettings
        {
            EmailNotifications = true,
            Tags = new List<string> { "VIP" }
        }
    };

    var clonedCustomer = original.Clone();

    clonedCustomer.Name = "Cloned Customer";


👉 Result:

- original untouched
- cloned is independent


#### ✅ 3. Do we implement shallow copy in real world?

👉 YES — but only in limited scenarios

Used when:

- objects are immutable
- DTOs
- stateless objects
- performance critical cases

Example:

    public Customer ShallowClone()
    {
        return (Customer)this.MemberwiseClone();
    }

🚨 Enterprise rule:

If object has:

- lists
- child objects
- aggregates

👉 ALWAYS deep copy

#### ✅ 4. Does .NET provide built-in prototype support?

👉 YES — via:

🔹 MemberwiseClone()

    protected object MemberwiseClone();


Characteristics:

- shallow copy
- protected method
- copies fields only

Example:

    public Customer Clone()
    {
        return (Customer)this.MemberwiseClone();
    }

🔹 ICloneable (⚠️ Not recommended)

    public interface ICloneable
    {
        object Clone();
    }


Why avoided in enterprise:

- unclear deep vs shallow
- returns object
- bad design contract

👉 Enterprise teams create:

    IPrototype<T>

instead.

#### ✅ Direct Answers Summary

|Question|	Answer|
|--------|--------|
|Mostly used for model/object creation?	| ✅ YES (construction problems)|
|Missing usage example? |	✔️ Provided (registry + direct clone)|
|Shallow copy used in real world?	|⚠️ Yes but limited cases|
|.NET built-in prototype?	|✔️ MemberwiseClone (shallow only)|



<br>


#### ✅ Is there an inbuilt deep clone method in .NET?

👉 NO single generic built-in deep clone exists like:

    DeepClone(obj)


.NET only gives:

    MemberwiseClone() → shallow copy

Deep cloning is implementation responsibility.

#### ✅ Real Ways to Do Deep Clone in .NET
##### 🔹 1. Manual Deep Copy (✅ Enterprise Preferred)

Most reliable, explicit, safe.

    public Customer Clone()
    {
        return new Customer
        {
            Name = this.Name,
            Address = new Address
            {
                City = this.Address.City,
                Country = this.Address.Country
            },
            Settings = new CustomerSettings
            {
                EmailNotifications = this.Settings.EmailNotifications,
                Tags = new List<string>(this.Settings.Tags)
            }
        };
    }


👉 Used in:

- Domain Driven Design
- Aggregates
- Business critical models

##### 🔹 2. Serialization Based Deep Clone (Built-in System.Text.Json)

Yes — this is the closest “built-in style” deep clone.

    using System.Text.Json;

    public static T DeepClone<T>(T obj)
    {
        var json = JsonSerializer.Serialize(obj);
        return JsonSerializer.Deserialize<T>(json);
    }

👍 Pros

- Simple
- automatic deep copy
- no manual mapping

👎 Cons (Enterprise Important)

- slow
- ignores private fields
- needs parameterless constructors
- loses EF tracking / proxies
- breaks polymorphic types unless configured

👉 Used only in:

- testing
- prototypes
- non-critical tools

##### 🔹 3. Binary Serialization (❌ Deprecated)

Old way — do NOT use.

    BinaryFormatter → banned

##### 🔹 4. Record Types (Modern C# — Partial Help)
    var copy = original with { Name = "New" };


**⚠️ This is NOT deep clone automatically**

Nested objects still reference same instance unless immutable.

🔥 Enterprise Reality (Very Important)

Real enterprise systems mostly use:

- 1️⃣ Manual deep clone
- 2️⃣ Mapping libraries (AutoMapper projection)
- 3️⃣ Domain copy constructors

NOT generic deep clone utilities.

##### ✅ Interview Level Answer

.NET does not provide a built-in deep clone API. MemberwiseClone performs only shallow copying. In enterprise applications deep cloning is typically implemented manually to ensure correctness of aggregates. Serialization-based cloning using System.Text.Json can be used for non-critical scenarios but is not recommended for production domain models due to performance and control limitations.

#### 🧠 One thing you’ll find interesting (advanced enterprise fact)

Many large companies actually avoid cloning completely and instead use:

- immutable objects
- copy constructors
- builders

because cloning can introduce hidden state bugs.


[.NET Built-In Prototyping](https://learn.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone?view=net-7.0)

We can do deep cloning as well but need to do manual changes, .NET does not provide any mechanism for deep clone.

Example:

    public Person ShallowCopy()
    {
        return (Person)MemberwiseClone();
    }

    public Person DeepCopy()
    {
        Person other = (Person)MemberwiseClone();
        other.IdInfo = new IdInfo(IdInfo.IdNumber);
        return other;
    }



Yeah — your instinct is mostly right 👍 but let’s make it accurate from an enterprise interview perspective, because saying “enterprise doesn’t use prototype” can sound slightly off.

### ✅ Reality: Prototype Pattern in Enterprise

👉 It is NOT heavily used as a formal pattern
especially compared to:

- Factory
- Builder
- Strategy
- Decorator

Because yes:

- deep cloning is expensive
- complex to maintain
- risky with mutable state
- EF tracked entities break easily
- reference bugs happen

So you’re absolutely right that it’s not common in daily business APIs.

### ✅ BUT — enterprises do use prototype concept indirectly

Not as “Prototype Pattern™” — but as copying templates / blueprints.

#### Real enterprise scenarios where prototype idea exists:
#### 🔹 1. Configuration Templates

    var defaultPolicy = new RetryPolicy(...);

    var userPolicy = defaultPolicy.Clone();
    userPolicy.Timeout = 30;


Used in:

- Polly
- HttpClient configuration
- Azure SDK options

#### 🔹 2. Game Engines / High Performance Systems

- object pools
- entity templates
- scene cloning

Heavy usage here.

#### 🔹 3. Workflow Engines / Rule Engines

    var workflow = defaultWorkflow.Clone();
    workflow.AssignUser("Yash");

#### 🔹 4. UI Builders / Form Designers

- default form template
- clone and customize

#### 🔹 5. Testing Infrastructure

Very common:

    var testUser = DefaultUsers.Admin.Clone();
    testUser.Email = "new@test.com";

### ✅ Where Enterprise Avoids Prototype

Most business backend systems prefer:

- Builders
- Factories
- Immutable models
- Mapping instead of cloning

Because cloning introduces:

- ❌ hidden side effects
- ❌ unclear ownership
- ❌ performance cost
- ❌ debugging nightmares

### 🔥 The PERFECT Interview Answer (very senior sounding)

#### Prototype pattern is conceptually useful but is not heavily used as a formal pattern in enterprise business applications due to complexity, deep copy overhead, and mutable state risks. However, the prototype concept appears in configuration templates, testing fixtures, workflow duplication, and object pooling scenarios. Most enterprise backend systems prefer builders, factories, or immutable copy constructors instead of general-purpose cloning.