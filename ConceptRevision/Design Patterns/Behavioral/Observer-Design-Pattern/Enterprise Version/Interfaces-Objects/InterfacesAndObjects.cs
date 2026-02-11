using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Design_Patterns.Behavioral.Observer_Design_Pattern.Enterprise_Version.Interfaces_Objects
{
   // 1 Event : This will be the trigger point and trigger reason

    public class OrderCreatedEvent {
    
        public Guid OrderId { get; set; }   
    }
    // 2 Observer Interface

    public interface IEventHandler<T>
    {
        Task Handle(T notification);
    }

}
