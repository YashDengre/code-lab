using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Design_Patterns.Structural.Proxy_Design_Pattern.Simple_Version.Interfaces_Objects
{
    // 1
    // Common Interface
    public interface IDcoumentService
    {
        string GetDocument(int id);
    }

    // 2
    // Real Service - Implementation
    public class DocumentService : IDcoumentService
    {
        public string GetDocument(int id)
        {
            Console.WriteLine("Fetching Document from DB....");
            return $"Document-{id}";
        }
    }
}
