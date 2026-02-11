using ConceptRevision.Design_Patterns.Structural.Decorator_Design_Pattern.EnterpriseVersion.Interfaces_Objects;

namespace ConceptRevision.Design_Patterns.Structural.Decorator_Design_Pattern.EnterpriseVersion
{
    public class OtherVaritions
    {
        public async static Task Process(int id)
        {
            // Interface
            // Class
            // Both will have same issue once implemented 
            // 1. If create a class which implements the interface as base then both will have issues like Object creation with null inner reference.
            // 2. if not considered as base then every decorator has to implement base and default behavior even if it is not required for that decorator.

            // Interface or Class Based:
            try
            {
                TransformDecorator transformDecorator = new TransformDecorator(null!);
                ConcreteClassmDecorator concreteClassDecorater = new ConcreteClassmDecorator(null!);
                await transformDecorator.GetAsync(id);
            }
            catch (Exception ex) {
            
                // This is one issue which can come in this variation (can come in abstract class version as well but we can not create obeject of base decorator as that is abstract class)
                // but we can inject null referece via any other decorator and apart from that other drawbacks are also there (Duplication of code) due to that we avoid this.
                Console.WriteLine("Error Occured: For interface and class based verison for decorator as inner reference is passed as null");
                Console.WriteLine(ex.Message);

                // Why not protect concrete class same as abstract class  liek validation or base object created if not passed.
                // Yes that we can do but still that will make sense - resonse: 
                // We can safeguard inner refernce in Concrete version also and pass ICustomerService c =  new ConcreteClassmDecorator(new CustomerService());
                // But conceptually this is nonsense:
                // It is not a decorator with added behavior, it is just a pointless wrapper.
                // This API allows something that shuold not exist.
                // Whereas Abstract class =  expressing the intent, not saftey: This is the key idea => Abstract does not mean safer = it means "Incomplete by design'.
                // It says: This class is only a helper, you must add behavior, Using me directly is meaningless.
                // That's a semantic gurantee, nota runtime one.
                // The real reason patterns prefer abstract here - Design patterns care about communicating intent to humans, not just machines.
                // Abstract decorator communicates:
                // ❌ “Do not use directly"
                // ✅ “Exists only to support subclasses”
                // ✅ “Incomplete by itself”
                // Concrete decorator communicates the opposite — even if technically locked down.

            }


        }
    }

    // 1.
    // Via Interface Directly either we can create an interface or use existing
    // We have ICustomerService, so will use same.

    // Direct Create the decorator

    public class TransformDecorator : ICustomerService
    {
        private readonly ICustomerService _inner;

        public TransformDecorator(ICustomerService customerService)
        {
            _inner = customerService;
        }

        public async virtual Task<Customer> GetAsync(int id)
        {
            Console.WriteLine("[Transofrm] Fetching the data:");
            return await _inner.GetAsync(id);
        }
    }

    // In this case we have ICustomer Service  and whenever we implement this we need to implement GetAsync
    // If we try to create the BaseDecorator out of ICustomer with abstratc class then it will become the abstract version and if we try to use 
    // this TransformDecorator/any name then it will act as a Concreate Class verison which will create issue like instantiating the class direct with null and will break the decorator chain.

    // for above scenario now we can create as many decorator but all wil need to implement the GetAsync()

    // 2 
    // Via Concreate Clas Directly which is also similar to above example:
    public class ConcreteClassmDecorator : ICustomerService
    {
        protected readonly ICustomerService _inner;

        public ConcreteClassmDecorator(ICustomerService customerService)
        {
            _inner = customerService;
        }

        public async virtual Task<Customer> GetAsync(int id)
        {
            Console.WriteLine("[Transofrm] Fetching the data:");
            return await _inner.GetAsync(id);
        }
    }

    // Now if use this Concrete class as base for other decorator then chain may break as object creation is allowed for this class

    public class ConcreteClassDecoratorV2 : ConcreteClassmDecorator
    {
        public ConcreteClassDecoratorV2(ICustomerService customerService) : base(customerService)
        {
        }

        public async virtual Task<Customer> GetAsync(int id)
        {
            Console.WriteLine("[Transofrm] Fetching the data:");
            return await _inner.GetAsync(id);
        }
    }


}
