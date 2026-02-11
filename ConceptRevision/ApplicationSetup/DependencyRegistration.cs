using ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern.Interfaces_Objects;
using Abstract_Fact = ConceptRevision.Design_Patterns.Creational.Abstract_Factory_Design_Pattern;
using Abstract_Fact_Obj = ConceptRevision.Design_Patterns.Creational.Abstract_Factory_Design_Pattern.Interfaces_Objetcs;
using ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static ConceptRevision.Design_Patterns.Creational.Factory_Design_Pattern.KeyedServicesFactory;
using ConceptRevision.Design_Patterns.Structural.Adapter_Design_Pattern.ThridPartySDK;
using ConceptRevision.Design_Patterns.Structural.Adapter_Design_Pattern.LegacySystem;
using ConceptRevision.Design_Patterns.Structural.Adapter_Design_Pattern;
using ConceptRevision.Design_Patterns.Structural.Decorator_Design_Pattern.Simple_Version;
using ConceptRevision.Design_Patterns.Structural.Decorator_Design_Pattern.EnterpriseVersion.Interfaces_Objects;
using ConceptRevision.Design_Patterns.Structural.Decorator_Design_Pattern.EnterpriseVersion;
using ConceptRevision.Design_Patterns.Structural.Proxy_Design_Pattern.Simple_Version.Interfaces_Objects;
using ConceptRevision.Design_Patterns.Structural.Proxy_Design_Pattern.Simple_Version;
using ProxyPattern = ConceptRevision.Design_Patterns.Structural.Proxy_Design_Pattern.Enterprise_Version;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.Caching;
using ConceptRevision.Design_Patterns.Structural.Facade_Design_Pattern.Interfaces_Objetcs;
using ConceptRevision.Design_Patterns.Structural.Facade_Design_Pattern;
using ConceptRevision.Design_Patterns.Behavioral.Chain_Of_Responsibility.Enterprise_Version;
using ConceptRevision.Design_Patterns.Creational.Prototype_Design_Pattern;
using ConceptRevision.Design_Patterns.Behavioral.Mediator_Deisgn_Pattern.Interfaces_Objects;
using ConceptRevision.Design_Patterns.Behavioral.Mediator_Deisgn_Pattern;
using ConceptRevision.Design_Patterns.Behavioral.Mediator_Deisgn_Pattern.Enterprise_Version;
using ConceptRevision.Design_Patterns.Behavioral.Observer_Design_Pattern.Enterprise_Version;
using ConceptRevision.Design_Patterns.Behavioral.Observer_Design_Pattern.Simple_Version;
using ConceptRevision.Design_Patterns.Behavioral.Observer_Design_Pattern.Enterprise_MediatR_Version;

namespace ConceptRevision.ApplicationSetup
{
    public static class DependencyRegistration
    {


        public static IHost Register()
        {
            var host = Host.CreateDefaultBuilder([]).ConfigureServices(services =>
            {

                #region Factory Pattern
                // 1
                services.AddScoped<CreditCardProcessor>();
                services.AddScoped<UpiProcessor>();
                services.AddScoped<IPaymentFactory, PaymentFactory>();
                // 2
                services.AddScoped<IPaymentProcessor, CreditCardProcessor>();
                services.AddScoped<IPaymentProcessor, UpiProcessor>();
                services.AddScoped<IPaymentFactoryOpenClosed, PaymentFactoryOpenClosed>();
                // 3
                services.AddScoped<IGenericPaymentFactory, GenericPaymentFactory>();
                // 4
                services.AddScoped<IPaymentProcessor, AttributeCreditCardProcessor>();
                services.AddScoped<IPaymentProcessor, AttributeUpiProcessor>();
                services.AddScoped<IAttributePaymentFactory, AttributePaymentFactory>();
                // 5
                services.AddKeyedScoped<IPaymentProcessor, UpiProcessor>("upi");
                services.AddKeyedScoped<IPaymentProcessor, CreditCardProcessor>("creditcard");
                services.AddScoped<PaymentService>();
                #endregion

                #region Abstract Factory Pattern
                services.AddTransient<Abstract_Fact_Obj.IPaymentPlatformFactory, Abstract_Fact_Obj.IndiaPaymentPlatformFactory>();
                services.AddTransient<Abstract_Fact_Obj.IPaymentPlatformFactory, Abstract_Fact_Obj.USPaymentPlatformFactory>();
                services.AddSingleton<Abstract_Fact.IPaymentFactoryResolver, Abstract_Fact.PaymentFactoryResolver>();
                services.AddScoped<Abstract_Fact.AbstractFactory>();
                #endregion

                #region Adaptor Patter

                services.AddScoped<StripeService>();
                services.AddScoped<LegacyPaymentClient>();
                services.AddScoped<StripePaymentAdapter>();
                services.AddScoped<LegacyPaymentAdapter>();
                services.AddKeyedScoped<IPaymentGateway, StripePaymentAdapter>("third-party-payment");
                services.AddKeyedScoped<IPaymentGateway, LegacyPaymentAdapter>("legacy-payment");

                //Class based adapater
                services.AddKeyedScoped<IPaymentGateway, StripePaymentAdapter_Class>("third-party-payment");
                services.AddKeyedScoped<IPaymentGateway, LegacyPaymentAdapter_Class>("legacy-payment");

                #endregion

                #region Decorator Pattern

                // Simple Version Decorator
                services.AddScoped<ICoffee, SimpleCofee>();
                services.AddScoped<MilkDecorator>();
                services.AddScoped<SugarDecorator>();
                //services.AddScoped<ICoffee, SimpleCofee>();
                //services.AddScoped<CoffeeDecorator, MilkDecorator>();
                //services.AddScoped<CoffeeDecorator, SugarDecorator>();

                // Enterprise Version Decorator
                services.AddScoped<ICustomerService, CustomerService>();
                services.AddScoped<ValidationCustomerService>();
                services.AddScoped<LoggingCustomerService>();
                //services.AddScoped<ICustomerService, ValidationCustomerService>();
                //services.AddScoped<ICustomerService, LoggingCustomerService>();
                // We can use Scrutor package as well to inject the decorator dependencies
                //services.Decorate<ICustomerService, ValidationCustomerService>();
                //services.Decorate<ICustomerService, LoggingCustomerService>();

                #endregion

                #region Proxy Pattern

                //Simple Version Proxy
                services.AddScoped<DocumentService>();
                services.AddScoped<DocumentProxy>();
                services.AddScoped<IDcoumentService, DocumentService>();
                services.AddKeyedScoped<IDcoumentService, DocumentProxy>("proxy");

                // Enterprise Version Decorator
                //Using Keyed Service for easy testing
                services.AddKeyedScoped<ProxyPattern.Interfaces_Objects.IPaymentGateway, ProxyPattern.Interfaces_Objects.StripeGateway>("base");
                services.AddScoped<ProxyPattern.Interfaces_Objects.IPaymentGateway, ProxyPattern.Interfaces_Objects.StripeGateway>();
                services.AddMemoryCache();
                services.AddKeyedScoped<ProxyPattern.Interfaces_Objects.IPaymentGateway, ProxyPattern.PaymentGatewayProxy>("proxy");

                #endregion

                #region Facade Pattern

                services.AddScoped<IFacadePaymentService, FacadePaymentService>();
                services.AddScoped<IFacadeInventoryService, FacadeInventoryService>();
                services.AddScoped<IFacadeFraudService, FacadeFraudService>();
                services.AddScoped<IFacadeShippingService, FacadeShippingService>();
                services.AddScoped<IOrderFacade, OrderProcessingFacade>();
                services.AddScoped<Facade>();

                #endregion

                #region Chain Of Responsibility Pattern

                services.AddScoped<IOrderHandler, ValidationHandler>();
                services.AddScoped<IOrderHandler, FraudHandler>();
                services.AddScoped<IOrderHandler, PaymentHandler>();
                services.AddScoped<IOrderHandler, OrderCreationHandler>();
                services.AddScoped<OrderPipeline>();
                services.AddScoped<EnterpriseCOR>(); // Added this for using in program.cs

                #endregion

                #region Prototype Pattern

                services.AddScoped<ICustomerPrototypeRegistry, CustomerPrototypeRegistry>();

                #endregion

                #region Mediator Pattern
                
                // Enterprise version
                services.AddScoped<IMediator, Mediator>();
                services.AddScoped<IRequestHandler<CreateCustomerCommand, int>, CreateCustomerHandler>();
                services.AddScoped<IRequestHandler<GetCustomerQuery, string>, GetCustomerHandler>();
                services.AddScoped<EnterpriseMediator>();

                // Enterprise pipeline behavior
                services.AddTransient(typeof(IPipelineBehavior<,>),typeof(LoggingBehavior<,>));
                services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));
                services.AddTransient(typeof(IPipelineBehavior<,>),typeof(TransactionBehavior<,>));
                services.AddScoped<MediatorV2>();
                services.AddScoped<EnterpriseMediatorPipeline>();

                #endregion

                #region
                
                // Simple Version
                services.AddScoped<SimpleObserver>();

                // Enterprise Version
                services.AddScoped<EnterpriseObserver>();

                // Enterprise Version with MediatR Package
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyRegistration).Assembly));
                services.AddTransient<Design_Patterns.Behavioral.Observer_Design_Pattern.Enterprise_MediatR_Version.OrderService>();
                services.AddScoped<EnterpriseObserverMediatR>();

                #endregion

            }).Build();
            return host;
        }
    }
}
