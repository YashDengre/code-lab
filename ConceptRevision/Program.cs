
using ConceptRevision;
using ConceptRevision.ApplicationSetup;
using ConceptRevision.Design_Patterns.Creational;
using ConceptRevision.Design_Patterns.Creational.Abstract_Factory_Design_Pattern;
using ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern;
using ConceptRevision.Design_Patterns.Structural.Adapter_Design_Pattern;
using ConceptRevision.Design_Patterns.Structural.Decorator_Design_Pattern.EnterpriseVersion;
using ConceptRevision.Design_Patterns.Structural.Decorator_Design_Pattern.Simple_Version;
using ConceptRevision.Design_Patterns.Structural.Proxy_Design_Pattern.Enterprise_Version;
using ProxyPattern = ConceptRevision.Design_Patterns.Structural.Proxy_Design_Pattern.Enterprise_Version.Interfaces_Objects;
using ConceptRevision.Design_Patterns.Structural.Proxy_Design_Pattern.Simple_Version;
using ConceptRevision.Design_Patterns.Structural.Proxy_Design_Pattern.Simple_Version.Interfaces_Objects;
using ConceptRevision.Questions;
using ConceptRevision.Utility;
using Microsoft.Extensions.DependencyInjection;
using static ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern.KeyedServicesFactory;
using ConceptRevision.Design_Patterns.Structural.Additional.Decorator_Proxy_Comaparison;
using ConceptRevision.Design_Patterns.Structural.Facade_Design_Pattern;
using ConceptRevision.Design_Patterns.Behavioral.Chain_Of_Responsibility.Simple_Version;
using ConceptRevision.Design_Patterns.Behavioral.Chain_Of_Responsibility.Enterprise_Version;
using ConceptRevision.Design_Patterns.Creational.Builder_Design_Pattern.Simple_Version;
using ConceptRevision.Design_Patterns.Creational.Builder_Design_Pattern.Enterpprise_Version;
using ConceptRevision.Design_Patterns.Creational.Prototype_Design_Pattern;
using ConceptRevision.Design_Patterns.Behavioral.Mediator_Deisgn_Pattern;
using ConceptRevision.Design_Patterns.Behavioral.Mediator_Deisgn_Pattern.Classic_Mediator;
using ConceptRevision.Design_Patterns.Behavioral.Mediator_Deisgn_Pattern.Enterprise_Version;
using ConceptRevision.Design_Patterns.Behavioral.Observer_Design_Pattern.Simple_Version;
using ConceptRevision.Design_Patterns.Behavioral.Observer_Design_Pattern.Enterprise_Version;
using ConceptRevision.Design_Patterns.Behavioral.Observer_Design_Pattern.Enterprise_MediatR_Version;

// Dependency Registration
var host = DependencyRegistration.Register();

// Linq Problems
LinqPlayGround.Run();

// Utility
//BulkUserCreation.DownloadFile();
//BulkUserCreation.UploadFile();

// Design Patterns

#region Singleton

Console.WriteLine("\nSingleton Pattern Outcome:\n");
SingletonDesignPattern.Process();

#endregion

#region Factory

IPaymentFactory _factory = host.Services.GetRequiredService<IPaymentFactory>();
IPaymentFactoryOpenClosed _factoryOpenClosed = host.Services.GetRequiredService<IPaymentFactoryOpenClosed>();
IGenericPaymentFactory _genricPaymentFactory = host.Services.GetRequiredService<IGenericPaymentFactory>();
IAttributePaymentFactory _attributePaymentFactory = host.Services.GetRequiredService<IAttributePaymentFactory>();
PaymentService _keyedPaymentService = host.Services.GetRequiredService<PaymentService>();

Console.WriteLine("\nFactory Pattern Outcome:\n");
var factoryDesign = new Factory(_factory, _factoryOpenClosed, _genricPaymentFactory, _attributePaymentFactory, _keyedPaymentService);
await factoryDesign.Process("upi", 200.00m);
await factoryDesign.Process("creditcard", 200.00m);

#endregion

#region Abstract Factory
Console.WriteLine("\nAbstract Factory Pattern Outcome:\n");
AbstractFactory abstractFactory = host.Services.GetRequiredService<AbstractFactory>();
abstractFactory.Process("India", 500);
abstractFactory.Process("US", 500);
#endregion

#region Adapter Pattern
Console.WriteLine("\nAdapter Pattern Pattern Outcome:\n");
// Object based adapter
var stripePaymentAdapter = host.Services.GetRequiredService<StripePaymentAdapter>();
AdapterExecutor adapterExecutor = new AdapterExecutor(stripePaymentAdapter);
await adapterExecutor.Process(200);
var legacyPaymentAdapter = host.Services.GetRequiredService<LegacyPaymentAdapter>();
adapterExecutor = new AdapterExecutor(legacyPaymentAdapter);
await adapterExecutor.Process(200);
// We can wrap the Adapter creation logic with Factory pattern so that creating object of adapter is easy.
// We can apply full factory pattern or we can use keyed services of .NET 8 - see DI registration
// use as below:
IPaymentGateway paymentGateway = host.Services.GetRequiredKeyedService<IPaymentGateway>("third-party-payment");
adapterExecutor = new AdapterExecutor(paymentGateway);
await adapterExecutor.Process(200);

paymentGateway = host.Services.GetRequiredKeyedService<IPaymentGateway>("legacy-payment");
adapterExecutor = new AdapterExecutor(paymentGateway);
await adapterExecutor.Process(200);
// Class based adapter
paymentGateway = host.Services.GetRequiredKeyedService<IPaymentGateway>("third-party-payment");
adapterExecutor = new AdapterExecutor(paymentGateway);
await adapterExecutor.Process(200);

paymentGateway = host.Services.GetRequiredKeyedService<IPaymentGateway>("legacy-payment");
adapterExecutor = new AdapterExecutor(paymentGateway);
await adapterExecutor.Process(200);
#endregion

#region Decorator Pattern
Console.WriteLine("\nDecorator Pattern Pattern Outcome:\n");
// Simple Version
Console.WriteLine("\nSimple Version:\n");
SimpleDecorator simpleDecorator = new SimpleDecorator(null!); // Passing Null Constructor so it will have SimpleCoffee as base
await simpleDecorator.Process();

var milkDecoratorr = host.Services.GetRequiredService<MilkDecorator>();
simpleDecorator = new SimpleDecorator(milkDecoratorr);
await simpleDecorator.Process();

var sugarDecorator = host.Services.GetRequiredService<SugarDecorator>();
simpleDecorator = new SimpleDecorator(sugarDecorator);
await simpleDecorator.Process();

// RealWorld Version:
Console.WriteLine("\nRealWorld Version:\n");
RealWorldDecorator realWDecorator = new RealWorldDecorator(null!);  // Passing Null Constructor so it will have base customerService
await realWDecorator.Process(1);

var loggingCSDecorator = host.Services.GetRequiredService<LoggingCustomerService>();
realWDecorator = new RealWorldDecorator(loggingCSDecorator);
await realWDecorator.Process(1);

var validationCSDecorator = host.Services.GetRequiredService<ValidationCustomerService>();
realWDecorator = new RealWorldDecorator(validationCSDecorator);
await realWDecorator.Process(1);
await realWDecorator.Process(0);

// Other variations like interface and concrete class:
Console.WriteLine("\nDecorator Patter: Other variations like interface and concrete class::\n");
await OtherVaritions.Process(2);
#endregion

#region Proxy Pattern
Console.WriteLine("\nProxy Pattern Pattern Outcome:\n");
// Simple Version
Console.WriteLine("\nSimple Version:\n");
var baseService = host.Services.GetRequiredService<DocumentService>();
SimpleProxy simpleProxy = new SimpleProxy(baseService);
simpleProxy.Process(1);
IDcoumentService proxySeervice = new DocumentProxy(baseService, "Admin");
simpleProxy = new SimpleProxy(proxySeervice);
simpleProxy.Process(1);
proxySeervice = new DocumentProxy(baseService, "User");
simpleProxy = new SimpleProxy(proxySeervice);
simpleProxy.Process(1);

// RealWorld Version:
Console.WriteLine("\nRealWorld Version:\n");
var baseProxyService = host.Services.GetRequiredKeyedService<ProxyPattern.IPaymentGateway>("base");
var realWProxy = new RealWorldProxy(baseProxyService);
await realWProxy.Process(1000, 1010);
await realWProxy.Process(1000, 1010);
Console.WriteLine("\nNotice above output: payment got processed two time even the order ids are same as there is not caching or validation added on the base service");
Console.WriteLine("\nNow see the output of Proxy Service where we saveed the result based on the order id and amound in cache");
var realWProxyService = host.Services.GetRequiredKeyedService<ProxyPattern.IPaymentGateway>("proxy");
realWProxy = new RealWorldProxy(realWProxyService);
await realWProxy.Process(1000, 1010);
await realWProxy.Process(1000, 1010);

// Comparison between Decorator and Proxy
Console.WriteLine("\nComparison between Decorator and Proxy\n");
await DecoratorAndProxyComparision.Process();
#endregion

#region Facade Pattern
Console.WriteLine("\nFacade Pattern Pattern Outcome:\n");
var facade = host.Services.GetRequiredService<Facade>();
await facade.Process(1, 1, 100, 1);
Console.WriteLine("-----------------------------------");
await facade.Process(1, 1, 10000, 1);
Console.WriteLine("-----------------------------------");
await facade.Process(1, 1, 100, 6);
Console.WriteLine("-----------------------------------");
await facade.Process(1, 1, 0, 1);
Console.WriteLine("-----------------------------------");
await facade.Process(1, 0, 100, 1);
#endregion

#region Chain Of Responsibility
Console.WriteLine("\nChain Of Responsibility Pattern Outcome:\n");
Console.WriteLine("\nSimple Version:\n");
await SimpleCOR.Process("Yash");
await SimpleCOR.Process("");
Console.WriteLine("\nEnterprise Version:\n");
var enterpriseVersion = host.Services.GetRequiredService<EnterpriseCOR>();
await enterpriseVersion.Process(1000);
Console.WriteLine("\n");
await enterpriseVersion.Process(100000);
Console.WriteLine("\nManual Pipeline and chain Created::\n");
await enterpriseVersion.ManualProcess(1000);
Console.WriteLine("\n");
await enterpriseVersion.ManualProcess(100000);
#endregion 

#region Builder Pattern
Console.WriteLine("\nBuilder Pattern Outcome:\n");
Console.WriteLine("\nSimple Version:\n");
await SimpleBuilder.Process();
Console.WriteLine("\nEnterprise Version-1: direct builder without base contract (interface)\n");
await EnterpriseBuilder.Process();
Console.WriteLine("\nEnterprise Version-2: builder with base contract (interface)\n");
await EnterpriseBuilderV2.Process();
#endregion

#region Prototype Pattern
Console.WriteLine("\nPrototype Pattern Outcome:\n");
var prototypeRegistry =  host.Services.GetRequiredService<ICustomerPrototypeRegistry>();
var prototype =  new Prototype(prototypeRegistry);
await prototype.Process("default", "Yash");
#endregion

#region Mediator Pattern
Console.WriteLine("\nMediator Pattern Outcome:\n");
var mediator = host.Services.GetRequiredService<EnterpriseMediator>();
await mediator.Process();
Console.WriteLine("\nClasic Mediator Version:\n");
await ClassicMediator.Process();
Console.WriteLine("\nEnterprise Mediator Pipeline Version:\n");
var mediatorV2 = host.Services.GetRequiredService<EnterpriseMediatorPipeline>();
await mediatorV2.Process();
#endregion

#region Observer Pattern
Console.WriteLine("\nObserver Pattern Outcome:\n");
Console.WriteLine("\nSimple Version:\n");
SimpleObserver simpleObserver = host.Services.GetRequiredService<SimpleObserver>();
await simpleObserver.Process();
Console.WriteLine("\nEnterprise version:\n");
EnterpriseObserver enterpriseObserver = host.Services.GetRequiredService<EnterpriseObserver>();
await enterpriseObserver.Process();
Console.WriteLine("\nEnterprise version with MediatR Library:\n");
var enterpriseMediatRVersion = host.Services.GetRequiredService<EnterpriseObserverMediatR>();
await enterpriseMediatRVersion.Process();
#endregion

Console.ReadLine();