using ConceptRevision.Design_Patterns.Behavioral.Mediator_Deisgn_Pattern.Interfaces_Objects;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ConceptRevision.Design_Patterns.Behavioral.Mediator_Deisgn_Pattern.Enterprise_Version
{
    public class EnterpriseMediatorPipeline
    {
        private readonly MediatorV2 _mediatorV2;
        public EnterpriseMediatorPipeline(MediatorV2 mediatorV2)
        {
            _mediatorV2 = mediatorV2;
        }
        public async Task Process()
        {
            await _mediatorV2.Send(new CreateCustomerCommand() { Name = "Yash" });
            Console.WriteLine("--------------------------------------------");
            await _mediatorV2.Send(new GetCustomerQuery() { Id = 1 });
        }
    }

    // This is Enterprise Mediator Pipeline Behavior -  actual Enterprise Reality
    // Classic Mediator: Components talk through mediator
    // Enterprise Mediator pipeline: Request -> Mediator -> Pipeline Behavior -> Handler
    // Problem it solves: In large systems every request needs: Validation, Logging, Authorization, Transactions, Performance monitoring, Retry, Exception Handling.
    // Without pipeline: Every handler duplicates the logic :(
    // WIth pipeline: Behavior -> Behavior -> Behavior -> Handler

    // Again we will build Customer CQRS example with pipeline

    // Using the same IRequest and IRequestHandler which we have created in earlier enterprise example.

    // 1 Request Contract: IRequest and IRequestHandler

    // 2 Pipeline Behavior Contract

    public interface IPipelineBehavior<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next);
    }

    // 3 Command : Using the earlier command CreateCustomerCommand

    // 4 Hanlder : Using the earlier handler CreateCustomerHandler


    // 5 Enterprise Behaviors

    // Validation

    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next)
        {
            Console.WriteLine("Validation executed!");
            return await next();
        }
    }

    // Logging
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next)
        {
            Console.WriteLine("[Log] Logging before!");
            var result = await next();
            Console.WriteLine("[Log] Logging After!");
            return result;
        }
    }

    // Transaction
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next)
        {
            Console.WriteLine("Transaction Started");
            var result = await next();
            Console.WriteLine("Transaction Committed");
            return result;
        }
    }


    // 6 Enterprise Mediator: 
    // In earlier version the role of mediator to resolve the handler based on the request(query, command)
    // But here: we have to do below things:
    //          * resolve the handler\
    //          * resolve all the beaviors which are created for IPipelineBehavior<IRequest<TResponse>, TResponse>
    //          * Call the all the behaviors before or after the handler execution

    //(Conceptual — real MediatR handles this internally)
    public class MediatorV2
    {
        private readonly IServiceProvider _serviceProvider;

        public MediatorV2(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var requestType = request.GetType();
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
            var handler = _serviceProvider.GetRequiredService(handlerType);

            var behaviorType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, typeof(TResponse));
            var behaviors = _serviceProvider.GetServices(behaviorType).Cast<dynamic>().Reverse();

            Func<Task<TResponse>> handlerDelegate = () => ((dynamic)handler).Handle((dynamic)request); // either use dynamic type in handler or get the method by handlerType.GetMethod("Handle")

            foreach (var behavior in behaviors)
            {
                var next = handlerDelegate;
                handlerDelegate = () => behavior.Handle((dynamic)request, next);
            }
            return await handlerDelegate();
        }
    }

}
