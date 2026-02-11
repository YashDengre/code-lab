using ConceptRevision.Design_Patterns.Structural.Proxy_Design_Pattern.Enterprise_Version.Interfaces_Objects;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Design_Patterns.Structural.Proxy_Design_Pattern.Enterprise_Version
{
    public class RealWorldProxy
    {
        private readonly IPaymentGateway _paymentGateway;
        public RealWorldProxy(IPaymentGateway paymentGateway)
        {
            _paymentGateway = paymentGateway;
        }
        public async Task Process(decimal amount, int orderId )
        {
            var result = await _paymentGateway.ProcessAsync(amount, orderId);
            Console.WriteLine($"Payment is processed: {result}");
        }
    }

    // 3 
    // Proxy Layer (Enterprise style)

    public class PaymentGatewayProxy : IPaymentGateway
    {
        private readonly IPaymentGateway _realGateway;
        private readonly IMemoryCache _cache;
        public PaymentGatewayProxy(IPaymentGateway realGateway, IMemoryCache cache)
        {
            _realGateway = realGateway;
            _cache = cache;
        }

        public async Task<bool> ProcessAsync(decimal amount, int orderId)
        {
            var cacheKey = $"payment-{amount}-{orderId}";
            if (_cache.TryGetValue(cacheKey, out bool cachedResult))
            {
                Console.WriteLine("Returning cached result");
                return cachedResult;
            }
            Console.WriteLine("Proxy Calling real gateway");
            var result = await _realGateway.ProcessAsync(amount, orderId);

            _cache.Set(cacheKey, result);
            return result;
        }
    }
}
