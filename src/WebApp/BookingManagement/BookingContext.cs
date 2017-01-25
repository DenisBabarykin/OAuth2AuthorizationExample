using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement
{
    public class BookingContext : DbContext
    {
        public const string connectionString = @"server=(localdb)\MSSQLLocalDB;Initial Catalog=bookingstore.mdf;Integrated Security=True;";

        public BookingContext()
            : base(connectionString)
        { }

        public DbSet<Booking> Bookings { get; set; }
    }
}
