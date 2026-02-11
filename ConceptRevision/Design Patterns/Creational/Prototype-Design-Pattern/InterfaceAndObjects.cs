using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Design_Patterns.Creational.Prototype_Design_Pattern
{
    // 1 Prototype contract 
    // Every prototype will implement this to enable prototyping capability, it is generic so it can work with every object
    public interface IPrototype<T>
    {
        T Clone();
        T ShallowClone();
        T ShallowCloneViaBuiltInMethod();
    }

    // 2  Domain Models / Class -> who objects we will clone through protoype
    public class Address
    {
        public string City { get; set; } = null!;
        public string Region { get; set; } = null!;
    }

    public class CustomerSettings
    {
        public bool EmailNotification { get; set; }
        public List<string> Tags { get; set; } = new();
    }

}
