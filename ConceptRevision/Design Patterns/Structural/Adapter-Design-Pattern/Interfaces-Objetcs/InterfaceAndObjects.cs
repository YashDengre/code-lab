namespace ConceptRevision.Design_Patterns.Structural.Adapter_Design_Pattern
{
    internal class InterfaceAndObjects
    {
    }
}

// 1
// Third Party / External System Code - we can not change

namespace ConceptRevision.Design_Patterns.Structural.Adapter_Design_Pattern.ThridPartySDK
{
    public class StripeService
    {
        public async Task<bool> MakeCharge(decimal total)
        {
            await Task.Delay(100);
            Console.WriteLine("Third Party Stripe Service - success");
            return true;
        }
    }

}

// Legacy System Code - we can not change
namespace ConceptRevision.Design_Patterns.Structural.Adapter_Design_Pattern.LegacySystem
{
    public class LegacyPaymentClient
    {
        public async Task<bool> ExecuteTranscations(decimal amount)
        {
            await Task.Delay(100);
            Console.WriteLine("Legacy System - success");
            return true;
        }
    }

}