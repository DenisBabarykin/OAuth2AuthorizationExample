using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement
{
    public class BookingManager
    {
        public Booking AddBooking(Booking booking)
        {
            using (var context = new BookingContext())
            {
                var savedBooking = context.Bookings.Add(booking);
                context.SaveChanges();
                return savedBooking;
            }
        }

        public List<Booking> GetUserBookings(string userLogin)
        {
            using (var context = new BookingContext())
            {
                return context.Bookings.Where(b => b.UserLogin == userLogin).ToList();
            }
        }
    }
}
