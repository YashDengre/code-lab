using ConceptRevision.Design_Patterns.Behavioral.Mediator_Deisgn_Pattern.Interfaces_Objects;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Design_Patterns.Behavioral.Mediator_Deisgn_Pattern
{
    public class EnterpriseMediator
    {
        private readonly IMediator _mediator;
        public EnterpriseMediator(IMediator mediator)
        {
            _mediator = mediator;    
        }
        public async Task Process()
        {
            Console.WriteLine("\nCalling Create Customer Handler via Mediator\n");
            var createResult = await _mediator.Send(new CreateCustomerCommand { Name = "Yash" });
            Console.WriteLine($"Result : {createResult}");

            Console.WriteLine("\nCalling Get Customer Handler via Mediator\n");
            var getResult = await _mediator.Send(new GetCustomerQuery() { Id = 1 });
            Console.WriteLine($"Result : {getResult}");
        }

    }

    // 3 Enterprise Mediator Implementation
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));

            dynamic handler = _serviceProvider.GetService(handlerType);
            dynamic handlerR = _serviceProvider.GetRequiredService(handlerType);
            return await handler?.Handle((dynamic)request);
        }
    }

    // 5 Command Handler: Will handle the command/request -> call service or save in db, etc.
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        public async Task<int> Handle(CreateCustomerCommand reqeust)
        {
            Console.WriteLine($"Cutomer Created: {reqeust.Name}");
            return 1;
        }
    }

    // 7 Query Handler: Will handle the query/request -> call service or get it from db, etc.

    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, string>
    {
        public async Task<string> Handle(GetCustomerQuery request)
        {
            return $"Customer-{request.Id}";
        }
    }
}
