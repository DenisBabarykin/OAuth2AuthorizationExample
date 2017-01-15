using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Bill
    {
        public Bill()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
            Price = 0.0m;
        }

        [Key]
        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public string Requisites { get; set; }

        public Guid BookingId { get; set; }
    }
}
