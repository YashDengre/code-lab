using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace ConceptRevision.Design_Patterns.Creational.Builder_Design_Pattern.Enterprise_Version.Interfaces_Objects
{
    // 1 Create a Model/Domain Entity:

    public class Order
    {
        public string CustomerId { get; }
        public List<string> Items { get; }
        public decimal Discount { get; }
        public decimal Tax { get; }
        public DateTime CreatedAt { get; }

        public Order(
            string customerId,
            List<string> items,
            decimal discount,
            decimal tax,
            DateTime createdAt)
        {
            CustomerId = customerId;
            Items = items;
            Discount = discount;
            Tax = tax;
            CreatedAt = createdAt;
        }
        public override string ToString()
        {
            return $"CustomerId: {CustomerId}, Discount: {Discount}, Tax: {Tax}, CreatedAt: {CreatedAt}";
        }
    }


    // 1 Create Domain Model (Enterprise Order) - Complex Object for which we wil create the builder
    public class OrderV2
    {
        public Guid Id { get; private set; }
        public string CustomerId { get; private set; }
        public List<string> Items { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal Tax { get; private set; }
        public decimal Discount { get; private set; }
        public decimal ShippingCost { get; private set; }
        public decimal Total { get; private set; }
        public string PaymentMethod { get; private set; }
        public DateTime CreatedOn { get; private set; }

        internal OrderV2(
            Guid id,
            string customerId,
            List<string> items,
            decimal subTotal,
            decimal tax,
            decimal discount,
            decimal shippingCost,
            decimal total,
            string paymentMethod,
            DateTime createdOn)
        {
            Id = id;
            CustomerId = customerId;
            Items = items;
            SubTotal = subTotal;
            Tax = tax;
            Discount = discount;
            ShippingCost = shippingCost;
            Total = total;
            PaymentMethod = paymentMethod;
            CreatedOn = createdOn;
        }
    }

}
