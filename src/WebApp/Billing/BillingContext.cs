using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing
{
    public class BillingContext : DbContext
    {
        public const string connectionString = @"server=(localdb)\MSSQLLocalDB;Initial Catalog=billingstore.mdf;Integrated Security=True;";

        public BillingContext()
            : base(connectionString)
        { }

        public DbSet<Bill> Bills { get; set; }
    }
}
