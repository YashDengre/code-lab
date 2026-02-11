using ConceptRevision.Design_Patterns.Behavioral.Chain_Of_Responsibility.Simple_Version.Interfaces_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Design_Patterns.Behavioral.Chain_Of_Responsibility.Simple_Version
{
    public class SimpleCOR
    {
        public static Task Process(string value)
        {
            try
            {
                var logging = new LoggingHandler();
                var validation = new ValidationHandler();
                var execution = new ExecutionHndler();

                logging.SetNext(validation).SetNext(execution);
                logging.Handle(new Request() { Name = value });
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error Occured:");
                Console.WriteLine(ex.Message);
            }
            return Task.CompletedTask;
        }
    }

    // 1 Create Base Handler

    public abstract class Handler
    {
        protected Handler _next;

        public Handler SetNext(Handler next)
        {
            _next = next;
            return next;
        }
        public virtual void Handle(Request request)
        {
            _next?.Handle(request);
        }
    }

    // 2 Create Concrete Handlers

    public class LoggingHandler : Handler
    {
        public override void Handle(Request request)
        {
            Console.WriteLine("[Log]: Logging the request");
            base.Handle(request);
        }
    }

    public class ValidationHandler : Handler
    {
        public override void Handle(Request request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new Exception("Invalid request");

            Console.WriteLine("[Validation]: Validation Passed");
            base.Handle(request);
        }
    }

    public class ExecutionHndler : Handler
    {
        public override void Handle(Request request)
        {
            Console.WriteLine("Executing business logic");
        }
    }
}
