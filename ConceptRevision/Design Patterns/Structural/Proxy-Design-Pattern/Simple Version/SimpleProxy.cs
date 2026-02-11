using ConceptRevision.Design_Patterns.Structural.Proxy_Design_Pattern.Simple_Version.Interfaces_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Design_Patterns.Structural.Proxy_Design_Pattern.Simple_Version
{
    // 4 Usage
    public class SimpleProxy
    {
        private readonly IDcoumentService _proxyService;
        public SimpleProxy(IDcoumentService proxyService)
        {
              _proxyService = proxyService;
        }
        public void Process(int id)
        {
            try
            {
                var result = _proxyService.GetDocument(id);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured in proxy call.");
                Console.WriteLine(ex.Message);
            }
        }
    }

    // 3
    // Proxy

    public class DocumentProxy : IDcoumentService
    {
        private readonly IDcoumentService _realservice;
        private readonly string _role;
        public DocumentProxy(IDcoumentService dcoumentService, string role)
        {
            _realservice = dcoumentService;
            _role = role;
        }
        public string GetDocument(int id)
        {
            if (_role != "Admin")
            {
                throw new UnauthorizedAccessException("Access Denied");
            }
            Console.WriteLine("Access granted via proxy");
            return _realservice.GetDocument(id);
        }
    }
}
