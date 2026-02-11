using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Design_Patterns.Creational.Prototype_Design_Pattern
{
    public class Prototype
    {
        private readonly ICustomerPrototypeRegistry _customerPrototypeRegistry;
        public Prototype(ICustomerPrototypeRegistry customerPrototypeRegistry)
        {
            _customerPrototypeRegistry = customerPrototypeRegistry;
        }
        public async Task Process(string templateKey, string name)
        {
            var customer = await CreateCustomerFromTemplate(templateKey, name);
            Console.WriteLine("Customer 1 Created with default template key:\n");
            Console.WriteLine($"Name: {customer.Name} | Id: {customer.Id} | Address: {customer.Address.City},{customer.Address.Region} | Settings: " +
                $"{customer.Settings.EmailNotification},{string.Join(",", customer.Settings.Tags)}");

            var customer2 = customer.Clone();
            Console.WriteLine("\nCustomer 2 Cloned from Customer 1:\n");
            Console.WriteLine($"Name: {customer2.Name} | Id: {customer2.Id} | Address: {customer2.Address.City},{customer2.Address.Region} | Settings: " +
               $"{customer2.Settings.EmailNotification},{string.Join(",", customer2.Settings.Tags)}");

            var customer3 = customer2.Clone();
            Console.WriteLine("\nCustomer 3 Cloned from Customer 2:\n");
            Console.WriteLine($"Name: {customer3.Name} | Id: {customer3.Id} | Address: {customer3.Address.City},{customer3.Address.Region} | Settings: " +
               $"{customer3.Settings.EmailNotification},{string.Join(",", customer3.Settings.Tags)}");

            Console.WriteLine("\n Updating Name(Value type) and City(Ref type) in each of the customer objects\nlets see if it udpates other objects or not, by the meaning of prototyping it should not update any other obejct");
            Console.WriteLine("In this context: Value type : Direct property of current object, Reference Type: Direct properties but they are themselves classes/objects");
            Console.WriteLine("\n These objects are cloned via Deep Clone mechanism hence, even if we update reference type values, it should not update it in other objects");


            customer.Name = "Updated Customer 1";
            customer.Address.City = "Udpated City 1";
            Console.WriteLine("Udpated Customer 1 Object: Lets see the values of each object:");
            Console.WriteLine($"Customer 1: Name: {customer.Name} | City: {customer.Address.City}");
            Console.WriteLine($"Customer 2: Name: {customer2.Name} | City: {customer2.Address.City}");
            Console.WriteLine($"Customer 3: Name: {customer3.Name} | City: {customer3.Address.City}");

            customer2.Name = "Updated Customer 2";
            customer2.Address.City = "Udpated City 2";
            Console.WriteLine("Udpated Customer 2 Object: Lets see the values of each object:");
            Console.WriteLine($"Customer 1: Name: {customer.Name} | City: {customer.Address.City}");
            Console.WriteLine($"Customer 2: Name: {customer2.Name} | City: {customer2.Address.City}");
            Console.WriteLine($"Customer 3: Name: {customer3.Name} | City: {customer3.Address.City}");

            customer3.Name = "Updated Customer 3";
            customer3.Address.City = "Udpated City 3";
            Console.WriteLine("Udpated Customer 3 Object: Lets see the values of each object:");
            Console.WriteLine($"Customer 1: Name: {customer.Name} | City: {customer.Address.City}");
            Console.WriteLine($"Customer 2: Name: {customer2.Name} | City: {customer2.Address.City}");
            Console.WriteLine($"Customer 3: Name: {customer3.Name} | City: {customer3.Address.City}");

            Console.WriteLine("\n\n--------------ShallowClone Demo:----------------\n\n");
            var shallowCustomer1 =  customer.ShallowClone();
            var shallowCustomer2 = shallowCustomer1.ShallowClone();
            Console.WriteLine("Cloned the customer 1 into shallowCustomer 1 and shallowCustoemr1 into shallowCustomer 2 via ShallowClone Method\n");
            Console.WriteLine("Lets try to update same in these objects as well\n");

            customer.Name = "Updated Customer 1 - after ShallowCloned";
            customer.Address.City = "Udpated City 1 - after ShallowCloned";
            Console.WriteLine("\nUdpated Customer 1 Object: Lets see the values of each object:");
            Console.WriteLine($"\nCustomer 1: Name: {customer.Name} | City: {customer.Address.City}");
            Console.WriteLine($"Shallow Customer 1: Name: {shallowCustomer1.Name} | City: {shallowCustomer1.Address.City}");
            Console.WriteLine($"Shallow Customer 2: Name: {shallowCustomer2.Name} | City: {shallowCustomer2.Address.City}");

            shallowCustomer1.Name = "Updated Shallow Customer 1";
            shallowCustomer1.Address.City = "Udpated Shallow City 1";
            Console.WriteLine("\nUdpated Shallow Customer 1 Object: Lets see the values of each object:");
            Console.WriteLine($"\nCustomer 1: Name: {customer.Name} | City: {customer.Address.City}");
            Console.WriteLine($"Shallow Customer 1: Name: {shallowCustomer1.Name} | City: {shallowCustomer1.Address.City}");
            Console.WriteLine($"Shallow Customer 2: Name: {shallowCustomer2.Name} | City: {shallowCustomer2.Address.City}");

            shallowCustomer2.Name = "Updated Shallow Customer 2";
            shallowCustomer2.Address.City = "Udpated Shallow City 2";
            Console.WriteLine("\nUdpated Shallow Customer 2 Object: Lets see the values of each object:");
            Console.WriteLine($"\nCustomer 1: Name: {customer.Name} | City: {customer.Address.City}");
            Console.WriteLine($"Shallow Customer 1: Name: {shallowCustomer1.Name} | City: {shallowCustomer1.Address.City}");
            Console.WriteLine($"Shallow Customer 2: Name: {shallowCustomer2.Name} | City: {shallowCustomer2.Address.City}");

            Console.WriteLine("--------------------------------------------------------------");
            var shallowCustomer3 = shallowCustomer2.ShallowCloneViaBuiltInMethod();
            Console.WriteLine("\nCloned the shallowCustomer 2 into shallowCustomer 3 via .NET in built methodMemberWiseClone()\n");
            Console.WriteLine("Lets try to update same in these objects as well\n");
            shallowCustomer2.Name = "Updated Shallow Customer 2 - After Shallow3";
            shallowCustomer2.Address.City = "Udpated Shallow City 2 - After Shallow3";
            Console.WriteLine("\nUdpated Shallow Customer 2 Object: Lets see the values of each object:");
            Console.WriteLine($"Shallow Customer 2: Name: {shallowCustomer2.Name} | City: {shallowCustomer2.Address.City}");
            Console.WriteLine($"Shallow Customer 3: Name: {shallowCustomer3.Name} | City: {shallowCustomer3.Address.City}");

            shallowCustomer3.Name = "Updated Shallow Customer 3";
            shallowCustomer3.Address.City = "Udpated Shallow City 3";
            Console.WriteLine("\nUdpated Shallow Customer 3 Object: Lets see the values of each object:");
            Console.WriteLine($"Shallow Customer 2: Name: {shallowCustomer2.Name} | City: {shallowCustomer2.Address.City}");
            Console.WriteLine($"Shallow Customer 3: Name: {shallowCustomer3.Name} | City: {shallowCustomer3.Address.City}");

        }

        public async Task<Customer> CreateCustomerFromTemplate(string templateKey, string name)
        {
            var customer = _customerPrototypeRegistry.GetTemmplate(templateKey);
            customer.Name = name;
            customer.Id = Guid.NewGuid();
            return await Task.FromResult(customer);
        }
    }
    // 3 Customer Aggregate (Prototype)

    public class Customer : IPrototype<Customer>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public CustomerSettings Settings { get; set; }
        public Customer Clone()
        {
            return new Customer()
            {
                Id = Guid.NewGuid(),
                Name = this.Name,
                Address = new Address
                {
                    City = this.Address.City,
                    Region = this.Address.Region
                },
                Settings = new CustomerSettings
                {
                    EmailNotification = this.Settings.EmailNotification,
                    Tags = new List<string>(this.Settings.Tags)
                }
            };

            // This is deep cloning — enterprise critical.
        }

        public Customer ShallowClone()
        {
            return new Customer()
            {
                Id = Guid.NewGuid(),
                Name = this.Name,
                Address = this.Address,
                Settings = this.Settings
            };

            // This is shallow cloning — enterprise - limited usage.
        }

        public Customer ShallowCloneViaBuiltInMethod()
        {
            return (Customer)this.MemberwiseClone();

            // This is shallow cloning — via Built .NET Method
        }
    }

    // 4 Prototype Registry (Very Enterprise)
    public interface ICustomerPrototypeRegistry
    {
        Customer GetTemmplate(string key);
    }
    public class CustomerPrototypeRegistry : ICustomerPrototypeRegistry
    {
        private readonly Dictionary<string, Customer> _templates;
        public CustomerPrototypeRegistry()
        {
            _templates = new Dictionary<string, Customer>()
            {
                ["default"] = new Customer()
                {

                    Name = "Template",
                    Address = new Address()
                    {

                        City = "Pune",
                        Region = "MH"
                    },
                    Settings = new CustomerSettings
                    {
                        EmailNotification = true,
                        Tags = new List<string>() { "NewCustomer" }
                    }
                }
            };
        }
        public Customer GetTemmplate(string key)
        {
            return _templates[key].Clone();
        }
    }
}
