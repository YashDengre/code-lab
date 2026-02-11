using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Design_Patterns.Behavioral.Chain_Of_Responsibility.Enterprise_Version.Interfaces_Objects
{

    // 1 Model to be use
    public class OrderContext
    {
        public string UserId { get; set; } = null!;
        public decimal Amount { get; set; }
        public bool IsFraudulent { get; set; }
        public bool PaymentSuccess { get; set; }
    }

}
