using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Design_Patterns.Behavioral.Mediator_Deisgn_Pattern.Interfaces_Objects
{
    
    // We will build Customer CQRS Style - not completely CQRS but same style
    
    // 1 Base Inrerfaces for Request and Handler
    // 1.1 Request Interfaces:

    public interface IRequest<IResponse> { }

    // 1.2 Handler Interfaces
    public interface IRequestHandler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }

    // 2 Mediator Interface
    // Takes the request and resolve the handler associted to it,

    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request); 
    }

    // 4 Customer Command : To be use as request

    public class CreateCustomerCommand: IRequest<int>
    {
        public string Name { get; set; } = null!;
    }


    // 6 Customer Query: Similar to command but use only for reading the data
    public class GetCustomerQuery : IRequest<string>
    {
        public int Id { get; set; }
    }
   
}
