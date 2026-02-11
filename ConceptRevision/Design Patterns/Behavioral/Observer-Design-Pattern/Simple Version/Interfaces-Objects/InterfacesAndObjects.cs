using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Design_Patterns.Behavioral.Observer_Design_Pattern.Simple_Version.Interfaces_Objects
{

    // 1 Observer interface

    public interface IObserver
    {
        void Update(string message);
    }
}
