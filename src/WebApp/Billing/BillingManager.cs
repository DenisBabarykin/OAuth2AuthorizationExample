using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing
{
    public class BillingManager
    {
        public Bill AddBill(decimal price, string requisites, Guid bookingId)
        {
            using (var context = new BillingContext())
            {
                var bill = context.Bills.Add(new Bill() { Price = price, Requisites = requisites, BookingId = bookingId });
                context.SaveChanges();
                return bill;
            }
        }

        public Bill RemoveBill(Guid billId)
        {
            using (var context = new BillingContext())
            {
                var bill = context.Bills.Remove(context.Bills.FirstOrDefault(b => b.Id == billId));
                context.SaveChanges();
                return bill;
            }
        }

        public Bill GetBill(Guid bookingId)
        {
            using (var context = new BillingContext())
            {
                return context.Bills.FirstOrDefault(b => b.BookingId == bookingId);
            }
        }
    }
}
